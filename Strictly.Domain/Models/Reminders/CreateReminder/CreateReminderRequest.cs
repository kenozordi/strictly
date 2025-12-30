using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Reminders.CreateReminder
{
    public class CreateReminderRequest
    {
        public Guid StreakId { get; set; }
        public Guid UserId { get; set; }
        public ReminderFrequency Frequency { get; set; }
        /// <summary>
        /// The time to send the reminder. it can be earlier than the due time of the checkin for the streak
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// 1=Sunday, 2=Monday, 3=Tuesday, 4=Wednesday, 5=Thursday, 6=Friday, 7=Saturday
        /// </summary>
        public int DayOfWeek { get; set; }
        /// <summary>
        /// 1-31
        /// </summary>
        public int DayOfMonth { get; set; }
        public string? Message { get; set; }
        public ReminderLevel Level { get; set; }
    }
}
