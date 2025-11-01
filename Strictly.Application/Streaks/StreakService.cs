using Strictly.Application.Users;
using Strictly.Domain.Models;
using Strictly.Domain.Models.Constants;
using Strictly.Domain.Models.Entities;
using Strictly.Domain.Models.Streak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<(int, string, object?)> CreateStreak(CreateStreakRequest createStreakRequest)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(createStreakRequest.UserId);
            if (user == null)
            {
                return ((int)HttpStatusCode.NotFound, "User does not exist", default);
            }

            var streak = new Streak()
            {
                Title = createStreakRequest.Title,
                Description = createStreakRequest.Description,
                Frequency = createStreakRequest.Frequency,
                UserId = createStreakRequest.UserId,
            };
            await _streakRepo.CreateStreak(streak);
            return ((int)HttpStatusCode.OK, "Streak Created successfully", default);
        }

        public async Task<(int, BaseResponse<List<Streak>>)> GetStreakByUserIdAsync(Guid userId)
        {
            var streaks = await _streakRepo.GetStreakByUserIdAsync(userId);
            return streaks.Count > 0
                ? ((int)HttpStatusCode.OK, new BaseResponse<List<Streak>>().Success(streaks))
                : ((int)HttpStatusCode.NotFound, new BaseResponse<List<Streak>>().Empty("No streaks found"));
        }
    }
}
