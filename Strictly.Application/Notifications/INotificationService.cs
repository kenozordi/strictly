using Strictly.Domain.Constants;
using Strictly.Domain.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Notifications
{
    public interface INotificationService
    {
        Task<ServiceResult> SendAsync(NotificationRequest notificationRequest);
    }
}
