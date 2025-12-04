using AutoMapper;
using Strictly.Application.Streaks;
using Strictly.Application.Users;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using Strictly.Domain.Models.Shared.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.CheckIns
{
    public class CheckInService : ICheckInService
    {
        protected readonly IStreakRepo _streakRepo;
        protected readonly ICheckInRepo _checkInRepo;
        protected readonly IUserRepo _userRepo;
        protected readonly IMapper _mapper;
        public CheckInService(IStreakRepo streakRepo, IUserRepo userRepo,
            IMapper mapper, ICheckInRepo checkInRepo)
        {
            _checkInRepo = checkInRepo;
            _streakRepo = streakRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreateCheckIn(CreateCheckInRequest createCheckInRequest)
        {
            var result = new CreateCheckInValidator().Validate(createCheckInRequest);
            if (!result.IsValid)
            {
                return ResponseHelper.ToBadRequest(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));
            }

            // validate userId
            var user = await _userRepo.GetUserAsync(createCheckInRequest.UserId);
            if (user is null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }
            // validate streakId
            var streak = await _streakRepo.GetStreak(createCheckInRequest.StreakId);
            if (streak is null)
            {
                return ResponseHelper.ToBadRequest("Streak does not exist");
            }

            var checkIn = _mapper.Map<CheckIn>(createCheckInRequest);
            var affectedRows = await _checkInRepo.CreateCheckIn(checkIn);
            return affectedRows > 0
                ? ResponseHelper.ToSuccess("Check-In created successfully")
                : ResponseHelper.ToUnprocessable("Failed to create Check-In, please try again later!");
        }

        public async Task<ServiceResult> GetActiveCheckInSchedule(Guid streakId)
        {
            // validate streakId
            var streak = await _streakRepo.GetStreak(streakId);
            if (streak is null)
            {
                return ResponseHelper.ToBadRequest("Streak does not exist");
            }

            var checkInHistory = await _checkInRepo.GetActiveCheckInSchedule(streakId);
            return checkInHistory.Count > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<CheckIn>>(checkInHistory))
                : ResponseHelper.ToEmpty("No Check-In to see here");
        }

    }
}
