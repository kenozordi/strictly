using AutoMapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Notifications;
using Strictly.Application.Reminders;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.Notifications;
using Strictly.Domain.Models.Reminders;
using Strictly.Worker.NotificationConsumer.Constants;
using Strictly.Worker.NotificationConsumer.Enums;

namespace Strictly.Worker.NotificationProducer.Workers
{
    public class ReminderConsumer : BackgroundService
    {
        private readonly ILogger<ReminderConsumer> _logger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly IReminderService _reminderService;
        private readonly INotificationService _notificationService;
        private readonly IDatabase _consumerDatabase;
        private readonly IMapper _mapper;

        public ReminderConsumer(ILogger<ReminderConsumer> logger,
            IConnectionMultiplexer redisCache, IReminderRepo reminderRepo,
            IMapper mapper, IReminderService reminderService,
            INotificationService notificationService)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _notificationService = notificationService;
            _reminderService = reminderService;
            _consumerDatabase = _redisCache.GetDatabase();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Reminder Consumer running at: {time}", DateTimeOffset.Now);
                }

                var subscriber = _redisCache.GetSubscriber();
                var channel = await subscriber.SubscribeAsync($"{CacheKey.Notifications}:{NotificationEvent.Reminder.ToString()}");
                channel.OnMessage(async msg =>
                {
                    var reminderNotification = JsonConvert.DeserializeObject<ReminderNotification>(msg.Message);
                    if (reminderNotification != null)
                    {
                        var notificationRequest = new NotificationRequest()
                        {
                            NotificationType = NotificationType.Email,
                            To = reminderNotification.UserEmail,
                            Subject = reminderNotification.StreakTitle,
                            Message = reminderNotification.Message
                        };
                        var notificationResponse = await _notificationService.SendAsync(notificationRequest);
                    }

                });

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
                
        }
    
    }
}
