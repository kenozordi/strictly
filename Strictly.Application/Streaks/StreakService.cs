using Strictly.Application.Interfaces;
using Strictly.Domain.Models.Entities;
using Strictly.Domain.Models.Streak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks
{
    public class StreakService : IStreakService
    {
        protected readonly IStreakRepo _streakRepo;
        protected readonly IUserRepo _userRepo;
        public StreakService(IStreakRepo streakRepo, IUserRepo userRepo)
        {
            _streakRepo = streakRepo;
            _userRepo = userRepo;
        }

        public async Task<(bool, string, object?)> CreateStreak(CreateStreakRequest createStreakRequest)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(createStreakRequest.UserId);
            if (user == null)
            {
                return (false, "User does not exist", default);
            }

            var streak = new Streak()
            {
                Title = createStreakRequest.Title,
                Description = createStreakRequest.Description,
                Frequency = createStreakRequest.Frequency,
                UserId = createStreakRequest.UserId,
            };
            await _streakRepo.CreateStreak(streak);
            return (true, "Streak Created successfully", default);
        }
    }
}
