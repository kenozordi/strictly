using Strictly.Domain.Enum;
using Strictly.Domain.Models.Notifications;
using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Reminders
{
    public class ReminderNotification : BaseNotification
    {
        public Guid ReminderId { get; set; }
        public Guid StreakId { get; set; }
        public string? StreakTitle { get; set; }

    }
}
