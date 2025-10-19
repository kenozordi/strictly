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
        Task<(bool, string, object?)> CreateStreak(CreateStreakRequest createStreakRequest);
    }
}
