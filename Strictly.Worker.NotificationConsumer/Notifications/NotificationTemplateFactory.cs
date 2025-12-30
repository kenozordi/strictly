using Microsoft.Extensions.Options;
using Strictly.Domain.Enum;
using Strictly.Infrastructure.Configuration.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Worker.NotificationConsumer.Notifications
{
    public class NotificationTemplateFactory
    {
        private readonly EmailSettings _emailSettings;

        public NotificationTemplateFactory(IOptions<EmailSettings> emailOptions)
        {
            _emailSettings = emailOptions.Value;
        }

        public string GetReminderNotificationTemplate(ReminderLevel reminderLevel)
        {
            switch (reminderLevel)
            {
                case ReminderLevel.Normal:
                    return _emailSettings.Templates.ReminderTemplate.Normal;
                case ReminderLevel.Warning:
                    return _emailSettings.Templates.ReminderTemplate.Warning;
                case ReminderLevel.Critical:
                    return _emailSettings.Templates.ReminderTemplate.Critical;
                default:
                    return _emailSettings.Templates.ReminderTemplate.Normal;
            }
        }
    }
}
