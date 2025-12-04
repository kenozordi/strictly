using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks
{
    public interface IStreakRepo
    {
        /// <summary>
        /// Create a new Streak
        /// </summary>
        /// <param name="streak"></param>
        /// <returns></returns>
        Task<int> CreateStreak(Streak streak);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Streak>> GetStreakByUserIdAsync(Guid userId);

        /// <summary>
        /// Get streak by streakId
        /// </summary>
        /// <param name="streakId"></param>
        /// <returns></returns>
        Task<Streak?> GetStreak(Guid streakId);
    }
}
