using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Strictly.Application.Notifications;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Notifications.AuditLoggers
{
    public class RedisAuditLogger : INotificationAuditLogger
    {
        private readonly ILogger<RedisAuditLogger> _logger;
        private readonly IConnectionMultiplexer _redisCache;
        private readonly JsonSerializerSettings _serializerSettings;

        public RedisAuditLogger(ILogger<RedisAuditLogger> logger,
            IConnectionMultiplexer redisCache,
            JsonSerializerSettings serializerSettings)
        {
            _logger = logger;
            _redisCache = redisCache;
            _serializerSettings = serializerSettings;
        }

        public async Task<bool> LogProcessedNotification(
            NotificationEvent notificationEvent,
            Domain.Enum.StrictlyClient notificationStage, 
            object notification)
        {
            var db = _redisCache.GetDatabase();
            var cacheStreamEntry = new[]
            {
                new NameValueEntry(
                    NotificationEvent.Reminder.ToString(),
                    JsonConvert.SerializeObject(notification, _serializerSettings))
            };
            var streamId = await db.StreamAddAsync(
                $"{CacheKey.AuditLog}:{NotificationEvent.Reminder.ToString()}:{notificationStage.ToString()}", 
                cacheStreamEntry);

            return string.IsNullOrEmpty(streamId.ToString()) ? false : true;
        }
    }
}
