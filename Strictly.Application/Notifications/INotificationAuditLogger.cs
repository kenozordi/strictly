using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Notifications
{
    /// <summary>
    /// Interface for the event driven tool/service used to produce/publish the notifications
    /// </summary>
    public interface INotificationAuditLogger
    {
        /// <summary>
        /// Log the result from processsing a notification like a reminder
        /// </summary>
        /// <returns></returns>
        Task<bool> LogProcessedNotification(
            NotificationEvent notificationEvent,
            Domain.Enum.StrictlyClient notificationStage,
            object notification);
    }
}
