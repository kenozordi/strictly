using Strictly.Domain.Constants;
using Strictly.Domain.Models.Streaks.CreateStreak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks
{
    public interface IStreakService
    {
        /// <summary>
        /// Create a new streak
        /// </summary>
        /// <param name="createStreakRequest"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateStreak(CreateStreakRequest createStreakRequest);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetStreakCreatedByUserIdAsync(Guid userId);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetStreaksByUserIdAsync(Guid userId);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="streakId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> AddStreakParticipant(Guid streakId, Guid userId);
    }
}
