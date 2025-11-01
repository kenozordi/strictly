using Strictly.Domain.Models;
using Strictly.Domain.Models.Entities;
using Strictly.Domain.Models.Streak;
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
        Task<(int, string, object?)> CreateStreak(CreateStreakRequest createStreakRequest);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<(int, BaseResponse<List<Streak>>)> GetStreakByUserIdAsync(Guid userId);
    }
}
