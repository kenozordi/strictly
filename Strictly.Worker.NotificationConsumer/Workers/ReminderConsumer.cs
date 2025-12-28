using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Notifications;
using Strictly.Application.Reminders;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.Notifications;
using Strictly.Domain.Models.Reminders;
using Strictly.Infrastructure.Configuration.AppSettings;

namespace Strictly.Worker.NotificationProducer.Workers
{
    public class ReminderConsumer : BackgroundService
    {
        private readonly NotificationEvent _NotificationEvent = NotificationEvent.Reminder;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly INotificationService _notificationService;
        private readonly INotificationAuditLogger _auditLogger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly ILogger<ReminderConsumer> _logger;
        private readonly IReminderRepo _reminderRepo;
        private readonly EmailSettings _emailSettings;
        private readonly IMapper _mapper;

        public ReminderConsumer(ILogger<ReminderConsumer> logger,
            IConnectionMultiplexer redisCache, IReminderRepo reminderRepo,
            IMapper mapper, IReminderService reminderService,
            INotificationService notificationService,
            INotificationAuditLogger auditLogger, JsonSerializerSettings serializerSettings,
            IOptions<EmailSettings> options)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _auditLogger = auditLogger;
            _reminderRepo = reminderRepo;
            _emailSettings = options.Value;
            _serializerSettings = serializerSettings;
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _redisCache.GetSubscriber();
            var channelName = $"{CacheKey.Notifications}:{NotificationEvent.Reminder.ToString()}";
            var channel = await subscriber.SubscribeAsync(channelName);
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Reminder Consumer running at: {time}", DateTimeOffset.Now);
                }

                channel.OnMessage(async msg =>
                {
                    var reminderNotification = JsonConvert.DeserializeObject<ReminderNotification>(msg.Message);
                    if (reminderNotification is null)
                    {
                        return;
                    }

                    var reminder = await _reminderRepo.GetReminder(reminderNotification.ReminderId);
                    if (reminder is null)
                    {
                        reminderNotification.UpdatedAt = DateTime.Now;
                        reminderNotification.Status = NotificationStatus.Unprocessible;
                        await _auditLogger.LogProcessedNotification(_NotificationEvent, Domain.Enum.StrictlyClient.Consumer, reminderNotification);

                        reminder!.UpdatedAt = DateTime.Now;
                        await _reminderRepo.UpdateReminder(reminder);
                        return;
                    }

                    if (reminder.UpdatedAt != default && DateTime.Now < reminder.UpdatedAt.Value.AddDays(1))
                    {
                        reminderNotification.UpdatedAt = DateTime.Now;
                        reminderNotification.Status = NotificationStatus.Expired;
                        await _auditLogger.LogProcessedNotification(_NotificationEvent, Domain.Enum.StrictlyClient.Consumer, reminderNotification);
                        return;
                    }
                    
                    if (DateTime.Now.TimeOfDay > reminderNotification.Time)
                    {
                        reminderNotification.UpdatedAt = DateTime.Now;
                        reminderNotification.Status = NotificationStatus.Expired;
                        await _auditLogger.LogProcessedNotification(_NotificationEvent, Domain.Enum.StrictlyClient.Consumer, reminderNotification);

                        reminder!.UpdatedAt = DateTime.Now;
                        await _reminderRepo.UpdateReminder(reminder);
                        return;
                    }

                    var emailTemplate = await File.ReadAllTextAsync(_emailSettings.Templates.BasePath + _emailSettings.Templates.Reminder);
                    emailTemplate = emailTemplate
                    .Replace("{{StreakTitle}}", reminderNotification.StreakTitle)
                    .Replace("{{ReminderMessage}}", reminderNotification.Message);

                    var notificationRequest = new NotificationRequest()
                    {
                        NotificationType = NotificationType.Email,
                        To = reminderNotification.UserEmail,
                        Subject = reminderNotification.StreakTitle,
                        Message = emailTemplate
                    };

                    var notificationResponse = await _notificationService.SendAsync(notificationRequest);

                    reminderNotification.UpdatedAt = DateTime.Now;
                    reminderNotification.Status = NotificationStatus.Queued;
                    await _auditLogger.LogProcessedNotification(_NotificationEvent, Domain.Enum.StrictlyClient.Consumer, reminderNotification);

                    reminder!.UpdatedAt = DateTime.Now;
                    await _reminderRepo.UpdateReminder(reminder);

                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing reminder message");
                throw;
            }

            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                await subscriber.UnsubscribeAsync(channelName);
                _logger.LogInformation("Reminder Consumer stopped.");
            }
        }
    
    }
}
