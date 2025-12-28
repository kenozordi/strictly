using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
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
        Task<ServiceResult> AddToQueue<T>(NotificationEvent notificationEvent, T notification);
        Task<ServiceResult> SendAsync(NotificationRequest notificationRequest);
    }
}
