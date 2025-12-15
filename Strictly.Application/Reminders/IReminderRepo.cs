using Strictly.Domain.Enum;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Reminders.CreateReminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Reminders
{
    public interface IReminderRepo
    {
        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns></returns>
        Task<int> CreateReminder(Reminder reminder);

        /// <summary>
        /// Get a Reminder
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        Task<Reminder?> GetReminder(Guid reminderId);

        /// <summary>
        /// Get active Reminder
        /// </summary>
        /// <returns></returns>
        Task<List<Reminder>> GetActiveReminders(ReminderFrequency reminderFrequency);

        /// <summary>
        /// Update a Reminder
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateReminder(Reminder reminder);
    }
}
