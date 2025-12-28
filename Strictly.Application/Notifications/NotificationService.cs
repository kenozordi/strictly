using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Reminders;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.Notifications;
using Strictly.Domain.Models.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationProviderFactory _notificationProviderFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly INotificationAuditLogger _auditLogger;
        private readonly ILogger<NotificationService> _logger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly IDatabase _producerDatabase;

        public NotificationService(INotificationProviderFactory notificationProviderFactory, 
            INotificationAuditLogger auditLogger, ILogger<NotificationService> logger,
             JsonSerializerSettings serializerSettings, IConnectionMultiplexer redisCache)
        {
            _logger = logger;
            _redisCache = redisCache;
            _auditLogger = auditLogger;
            _serializerSettings = serializerSettings;
            _producerDatabase = _redisCache.GetDatabase();
            _notificationProviderFactory = notificationProviderFactory;
        }

        public async Task<ServiceResult> AddToQueue<T>(NotificationEvent notificationEvent, T notification)
        {
            _logger.LogInformation("About to add notification to queue: {@reminderNotification}", notification);

            BaseNotification baseNotification = notification as BaseNotification;
            if (baseNotification is null) 
            { 
                await _auditLogger.LogProcessedNotification(notificationEvent, StrictlyClient.Producer, notification);
            }

            var clients = await _producerDatabase.PublishAsync(
                $"{CacheKey.Notifications}:{notificationEvent.ToString()}",
                JsonConvert.SerializeObject(notification, _serializerSettings));

            return clients > 0
                ? ResponseHelper.ToSuccess(clients)
                : ResponseHelper.ToUnprocessable("Something went wrong adding notification to queue: no client is subscribed to the queue");
        }

        public async Task<ServiceResult> SendAsync(NotificationRequest notificationRequest)
        {
            _logger.LogInformation("About to send notification : {@notification}", notificationRequest);

            var notificationProvider = await _notificationProviderFactory.GetNotificationProvider(notificationRequest.NotificationType);
            return await notificationProvider.SendAsync(notificationRequest);

        }
    }
}
