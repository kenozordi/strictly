using Strictly.Domain.Constants;
using Strictly.Domain.Models.Notifications;
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

        public NotificationService(INotificationProviderFactory notificationProviderFactory)
        {
            _notificationProviderFactory = notificationProviderFactory;
        }

        public async Task<ServiceResult> SendAsync(NotificationRequest notificationRequest)
        {
            var notificationProvider = await _notificationProviderFactory.GetNotificationProvider(notificationRequest.NotificationType);
            return await notificationProvider.SendAsync(notificationRequest);

        }
    }
}
