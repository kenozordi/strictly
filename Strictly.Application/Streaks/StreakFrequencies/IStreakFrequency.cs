using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks.StreakFrequencies
{
    public interface IStreakFrequency
    {
        /// <summary>
        /// Get the next checkin date starting from the date specified
        /// </summary>
        /// <param name="streak"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<DateTime> GetNextCheckInDate(Streak streak, DateTime date);

        /// <summary>
        /// Generate the checkin schedule and save in database
        /// </summary>
        /// <param name="streak"></param>
        /// <param name="firstCheckInDate"></param>
        /// <returns></returns>
        Task<(bool isSuccess, string message)> AddCheckInSchedule(Streak streak, DateTime firstCheckInDate);
    }
}
