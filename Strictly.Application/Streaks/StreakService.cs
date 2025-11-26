using Strictly.Application.Users;
using Strictly.Domain.Models;
using Strictly.Domain.Models.Shared.Constants;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<(int statusCode, dynamic responseBody)> CreateStreak(CreateStreakRequest createStreakRequest)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(createStreakRequest.UserId);
            if (user == null)
            {
                return ((int)HttpStatusCode.BadRequest, ResponseHelper.ToBadRequest("User does not exist"));
            }

            var streak = new Streak()
            {
                Title = createStreakRequest.Title,
                Description = createStreakRequest.Description,
                Frequency = createStreakRequest.Frequency,
                UserId = createStreakRequest.UserId,
            };
            var affectedRows = await _streakRepo.CreateStreak(streak);
            return affectedRows > 0
                ? ((int)HttpStatusCode.OK, ResponseHelper.ToSuccess("Streak created successfully"))
                : ((int)HttpStatusCode.UnprocessableEntity, ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!"));
        }

        public async Task<(int statusCode, dynamic responseBody)> GetStreakByUserIdAsync(Guid userId)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return ((int)HttpStatusCode.BadRequest, ResponseHelper.ToBadRequest("User does not exist"));
            }

            var streaks = await _streakRepo.GetStreakByUserIdAsync(userId);
            return streaks.Count > 0
                ? ((int)HttpStatusCode.OK, ResponseHelper.ToSuccess(streaks))
                : ((int)HttpStatusCode.NotFound, ResponseHelper.ToEmpty("No streaks found"));
        }
    }
}
