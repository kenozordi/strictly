using Strictly.Domain.Models.StreakParticipants;
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
        Task<List<Streak>> GetStreakCreatedByUserIdAsync(Guid userId);

        /// <summary>
        /// Get the streaks a userId is particiapting in. [Personal and group streaks]
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<StreakParticipant>> GetStreakParticipationByUserIdAsync(Guid userId);

        /// <summary>
        /// Get streak by streakId
        /// </summary>
        /// <param name="streakId"></param>
        /// <returns></returns>
        Task<Streak?> GetStreak(Guid streakId);

        /// <summary>
        /// Get streak participant
        /// </summary>
        /// <param name="streakId"></param>
        /// <returns></returns>
        Task<StreakParticipant?> GetStreakParticipant(Guid streakId, Guid userId);

        /// <summary>
        /// Add a user to participate in a streak
        /// </summary>
        /// <param name="streakParticipant"></param>
        /// <returns></returns>
        Task<int> AddStreakParticipant(StreakParticipant streakParticipant);
    }
}
