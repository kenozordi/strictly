using AutoMapper;
using Strictly.Application.CheckIns;
using Strictly.Application.Users;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using Strictly.Domain.Models.Shared.Constants;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Streaks.CreateStreak;
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
        protected readonly ICheckInService _checkInService;
        protected readonly IMapper _mapper;
        public StreakService(IStreakRepo streakRepo, IUserRepo userRepo,
            IMapper mapper, ICheckInService checkInService)
        {
            _checkInService = checkInService;
            _streakRepo = streakRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreateStreak(CreateStreakRequest createStreakRequest)
        {
            var result = new CreateStreakValidator().Validate(createStreakRequest);
            if (!result.IsValid)
            {
                return ResponseHelper.ToBadRequest(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));
            }

            // validate userId
            var user = await _userRepo.GetUserAsync(createStreakRequest.UserId);
            if (user == null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }

            var streak = _mapper.Map<Streak>(createStreakRequest);
            var affectedRows = await _streakRepo.CreateStreak(streak);
            if (affectedRows <= 0)
            {
                return ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!");
            }

            var checkInScheduleResult = await _checkInService.CreateCheckInSchedule(streak);
            return checkInScheduleResult.IsSuccess
                ? ResponseHelper.ToSuccess("Streak created successfully")
                : ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!"); // rollback streak
        }

        public async Task<ServiceResult> GetStreakByUserIdAsync(Guid userId)
        {
            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }

            var streaks = await _streakRepo.GetStreakByUserIdAsync(userId);
            return streaks.Count > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<GetStreakResponse>>(streaks))
                : ResponseHelper.ToUnprocessable("No streaks found");
        }
    }
}
