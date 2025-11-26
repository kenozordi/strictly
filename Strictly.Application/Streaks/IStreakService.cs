using Strictly.Domain.Models.Streaks;
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
        Task<(int, dynamic)> CreateStreak(CreateStreakRequest createStreakRequest);

        /// <summary>
        /// Get streak by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><(HTTP StatusCode, success/failed response)></returns>
        Task<(int, dynamic)> GetStreakByUserIdAsync(Guid userId);
    }
}
