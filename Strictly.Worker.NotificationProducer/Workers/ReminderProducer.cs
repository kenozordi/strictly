using AutoMapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Notifications;
using Strictly.Application.Reminders;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Shared;
using System.Collections.Generic;

namespace Strictly.Worker.NotificationProducer.Workers
{
    public class ReminderProducer : BackgroundService
    {
        private readonly NotificationEvent _NotificationEvent = NotificationEvent.Reminder;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly INotificationAuditLogger _auditLogger;
        private readonly ILogger<ReminderProducer> _logger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly IReminderService _reminderService;
        private readonly INotificationService _notificationService;
        private readonly IDatabase _producerDatabase;
        private readonly IMapper _mapper;

        public ReminderProducer(ILogger<ReminderProducer> logger,
            IConnectionMultiplexer redisCache, INotificationService notificationService,
            IMapper mapper, IReminderService reminderService,
            INotificationAuditLogger auditLogger, JsonSerializerSettings serializerSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _auditLogger = auditLogger;
            _notificationService = notificationService;
            _reminderService = reminderService;
            _producerDatabase = _redisCache.GetDatabase();
            _serializerSettings = serializerSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.WhenAll([
                    ProduceReminders(ReminderFrequency.Daily)
                    ]);

                // Delay
                await Task.Delay(5000, stoppingToken);
            }
        }

        protected async Task ProduceReminders(ReminderFrequency reminderFrequency)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Reminder Producer running at: {time}", DateTimeOffset.Now);
                }

                var remindersResponse = await _reminderService.GetDueReminders(reminderFrequency);
                if (!remindersResponse.IsSuccess)
                {
                    return;
                }

                List<Reminder> reminders = (List<Reminder>)((BaseResponse<object>)remindersResponse.Data).Data;
                if (reminders is null || reminders.Count < 1)
                {
                    return;
                }

                foreach (var reminder in reminders)
                {
                    var reminderNotification = _mapper.Map<ReminderNotification>(reminder);
                    var result = await _notificationService.AddToQueue(_NotificationEvent, reminderNotification);

                    reminderNotification.UpdatedAt = DateTime.Now;
                    reminderNotification.Status = result.IsSuccess ? NotificationStatus.Queued : NotificationStatus.Unprocessible;
                    reminderNotification.ErrorMessage = result.IsSuccess ? string.Empty : $"{result.Error.Title}:{result.Error.Detail}";
                    await _auditLogger.LogProcessedNotification(_NotificationEvent, StrictlyClient.Producer, reminderNotification); // add status, errormessage to logs

                    _logger.LogInformation("Result from adding reminder to queue. {@result}", result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception occured");
                throw;
            }
        }
        

    }
}
