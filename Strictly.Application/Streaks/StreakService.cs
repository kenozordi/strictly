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

        public async Task<BaseResponse<string>> CreateStreak(CreateStreakRequest createStreakRequest)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(createStreakRequest.UserId);
            if (user == null)
            {
                return new BaseResponse<string>().Empty("User does not exist");
            }

            var streak = new Streak()
            {
                Title = createStreakRequest.Title,
                Description = createStreakRequest.Description,
                Frequency = createStreakRequest.Frequency,
                UserId = createStreakRequest.UserId,
            };
            await _streakRepo.CreateStreak(streak);
            return new BaseResponse<string>().Empty("Streak Created successfully");
        }

        public async Task<BaseResponse<List<Streak>>> GetStreakByUserIdAsync(Guid userId)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return new BaseResponse<List<Streak>>().Empty("User does not exist");
            }

            var streaks = await _streakRepo.GetStreakByUserIdAsync(userId);
            return streaks.Count > 0
                ? new BaseResponse<List<Streak>>().Success(streaks)
                : new BaseResponse<List<Streak>>().Empty("No streaks found");
        }
    }
}
