using Microsoft.Extensions.DependencyInjection;
using Strictly.Application.Notifications;
using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.NotificationProviders
{
    public class NotificationProviderFactory : INotificationProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public NotificationProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<INotificationProvider> GetNotificationProvider(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Email:
                    return _serviceProvider.GetRequiredService<MailkitProvider>();
                default:
                    throw new NotImplementedException("No implementation to handle this type of notification");
            }
        }
    }
}
