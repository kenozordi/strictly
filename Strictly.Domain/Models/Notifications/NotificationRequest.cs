using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Notifications
{
    public class NotificationRequest
    {
        public NotificationType NotificationType { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
