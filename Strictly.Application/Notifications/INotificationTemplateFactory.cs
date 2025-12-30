using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Notifications
{
    public interface INotificationTemplateFactory
    {
        string GetReminderTemplatePath(ReminderLevel reminderLevel);
    }
}
