using AutoMapper;
using Microsoft.VisualBasic;
using Strictly.Application.Streaks;
using Strictly.Application.Users;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using Strictly.Domain.Models.Shared.Constants;
using Strictly.Domain.Models.Streaks;
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

        public async Task<ServiceResult> CreateCheckInSchedule(Streak streak)
        {
            var addScheduleResponse = (false, "Unsuccessful");
            switch (streak.Frequency)
            {
                case Domain.Models.Shared.Enum.StreakFrequency.Daily:
                    addScheduleResponse = await AddDailyCheckInSchedule(streak);
                    break;
                case Domain.Models.Shared.Enum.StreakFrequency.Weekly:
                    throw new NotImplementedException();
                    break;
                case Domain.Models.Shared.Enum.StreakFrequency.Monthly:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return addScheduleResponse.Item1
                ? ResponseHelper.ToSuccess("Check-In schedule generated successfully")
                : ResponseHelper.ToUnprocessable($"Check-In schedule failed to generate: {addScheduleResponse.Item2}");
        }
        
        private async Task<(bool isSuccess, string message)> AddDailyCheckInSchedule(Streak streak)
        {
            // Todo: Wrap this in a DB transaction
            try
            {
                int noOfCheckinCreated = 0;
                int today = 1;
                int numOfCheckinsAfterToday = today + (streak.EndDate?.Date - streak.CreatedAt.Date)!.Value.Days;
                var nextDatePlaceholder = streak.CreatedAt;
                for (int i = 0; i < numOfCheckinsAfterToday; i++)
                {
                    var checkIn = new CheckIn()
                    {
                        DueDate = nextDatePlaceholder,
                        StreakId = streak.Id,
                        UserId = streak.UserId,
                        Status = (int)CheckInStatus.Pending
                    };
                    noOfCheckinCreated += await _checkInRepo.CreateCheckIn(checkIn);

                    nextDatePlaceholder = nextDatePlaceholder.AddDays(1);
                }

                return (true, noOfCheckinCreated.ToString());
            }
            catch (Exception ex)
            {
                return (false, "Something went wrong, An Exception Occured");
            }
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
