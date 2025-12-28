using Strictly.Domain.Constants;
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
    public interface IReminderService
    {
        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <param name="createReminderRequest"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateReminder(CreateReminderRequest createReminderRequest);

        /// <summary>
        /// Send a reminder
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        Task<ServiceResult> SendReminder(Guid reminderId);

        /// <summary>
        /// GetActiveReminders
        /// </summary>
        /// <param name="reminderFrequency"></param>
        /// <returns></returns>
        Task<ServiceResult> GetDueReminders(ReminderFrequency reminderFrequency);
    }
}
