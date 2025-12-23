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
        private readonly IReminderRepo _reminderRepo;
        private readonly IDatabase _producerDatabase;
        private readonly IMapper _mapper;

        public ReminderProducer(ILogger<ReminderProducer> logger,
            IConnectionMultiplexer redisCache, IReminderRepo reminderRepo,
            IMapper mapper, IReminderService reminderService,
            INotificationAuditLogger auditLogger, JsonSerializerSettings serializerSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _auditLogger = auditLogger;
            _reminderRepo = reminderRepo;
            _reminderService = reminderService;
            _producerDatabase = _redisCache.GetDatabase();
            _serializerSettings = serializerSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.WhenAll([
                    ProduceDailyReminders(),
                    //ProduceMonthlyReminders(),
                    ]);

                // Delay
                await Task.Delay(5000, stoppingToken);
            }
        }

        protected async Task ProduceDailyReminders()
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Reminder Producer running at: {time}", DateTimeOffset.Now);
                }

                var remindersResponse = await _reminderService.GetDueReminders(ReminderFrequency.Daily);
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
                    var clients = await _producerDatabase.PublishAsync(
                        $"{CacheKey.Notifications}:{_NotificationEvent.ToString()}",
                        JsonConvert.SerializeObject(reminderNotification, _serializerSettings));

                    reminderNotification.UpdatedAt = DateTime.Now;
                    reminderNotification.Status = NotificationStatus.Queued;
                    await _auditLogger.LogProcessedNotification(_NotificationEvent, NotificationStage.Producer, reminderNotification); // add status, errormessage to logs
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        protected async Task ProduceMonthlyReminders()
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Monthly Reminder Producer running at: {time}", DateTimeOffset.Now);
                }

                var remindersResponse = await _reminderService.GetDueReminders(ReminderFrequency.Monthly);
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
                    var notificationReminder = _mapper.Map<ReminderNotification>(reminder);
                    var clients = await _producerDatabase.PublishAsync(
                        $"{CacheKey.Notifications}:{NotificationEvent.Reminder.ToString()}",
                        JsonConvert.SerializeObject(notificationReminder, _serializerSettings));

                    notificationReminder.UpdatedAt = DateTime.Now;
                    notificationReminder.Status = NotificationStatus.Queued;
                    await _auditLogger.LogProcessedNotification(_NotificationEvent, NotificationStage.Producer, notificationReminder); // add status, errormessage to logs
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
