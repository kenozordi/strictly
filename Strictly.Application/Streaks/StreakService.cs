using AutoMapper;
using Microsoft.Extensions.Logging;
using Strictly.Application.CheckIns;
using Strictly.Application.Streaks.StreakFrequencies;
using Strictly.Application.Users;
using Strictly.Domain.Constants;
using Strictly.Domain.Models.StreakParticipants;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Streaks.CreateStreak;
using Strictly.Domain.Models.Users;
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
        protected readonly ILogger<StreakService> _logger;
        protected readonly IStreakFrequencyFactory _streakFrequencyFactory;
        public StreakService(IStreakRepo streakRepo, IUserRepo userRepo,
            IMapper mapper, ICheckInService checkInService, ILogger<StreakService> logger,
            IStreakFrequencyFactory streakFrequency)
        {
            _streakFrequencyFactory = streakFrequency;
            _checkInService = checkInService;
            _streakRepo = streakRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
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

            // create streak
            var streak = _mapper.Map<Streak>(createStreakRequest);
            var affectedRows = await _streakRepo.CreateStreak(streak);
            if (affectedRows <= 0)
            {
                return ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!");
            }

            // add streak participation
            var streakparticipant = new StreakParticipant()
            {
                StreakId = streak.Id,
                UserId = user.Id,
            };
            affectedRows = await _streakRepo.AddStreakParticipant(streakparticipant);
            if (affectedRows <= 0)
            {
                return ResponseHelper.ToUnprocessable("Failed to add user to streak, please try again later!");
            }

            // create check-in schedule
            var streakFrequencyService = _streakFrequencyFactory.GetStreakFrequencyService(streak.Frequency);
            var firstCheckInDate = await streakFrequencyService.GetNextCheckInDate(streak, DateTime.Now);
            var (isSuccess, message) = await streakFrequencyService.AddCheckInSchedule(streak, firstCheckInDate);
            return isSuccess
                ? ResponseHelper.ToSuccess("Streak created successfully")
                : ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!"); // rollback streak
        }
        
        public async Task<ServiceResult> AddStreakParticipant(Guid streakId, Guid userId)
        {
            _logger.LogInformation("About to AddStreakParticipant for {userId} and {streakId}", userId, streakId);

            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }
            // validate streak
            var streak = await _streakRepo.GetStreak(streakId);
            if (streak == null)
            {
                return ResponseHelper.ToBadRequest("streak does not exist");
            }
            if (streak.UserId == userId)
            {
                return ResponseHelper.ToBadRequest("User is already in this streak");
            }

            // validate that same user streak pair dosent exist on streakparticipant table
            var streakParticipant = await _streakRepo.GetStreakParticipant(streakId, userId);
            if (streakParticipant is not null)
            {
                return ResponseHelper.ToBadRequest("User is already in this streak");
            }

            // add streak participation
            var streakparticipant = new StreakParticipant()
            {
                StreakId = streakId,
                UserId = userId,
            };
            var affectedRows = await _streakRepo.AddStreakParticipant(streakparticipant);
            if (affectedRows <= 0)
            {
                return ResponseHelper.ToUnprocessable("Failed to add user to streak, please try again later!");
            }

            // create check-in schedule
            var streakFrequencyService = _streakFrequencyFactory.GetStreakFrequencyService(streak.Frequency);
            var firstCheckInDate = await streakFrequencyService.GetNextCheckInDate(streak, DateTime.Now);
            var (isSuccess, message) = await streakFrequencyService.AddCheckInSchedule(streak, firstCheckInDate);
            return isSuccess
                ? ResponseHelper.ToSuccess("Streak created successfully")
                : ResponseHelper.ToUnprocessable("Failed to create streak, please try again later!"); // rollback streak

        }

        public async Task<ServiceResult> GetStreakCreatedByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("About to GetStreakCreatedByUserIdAsync for {userId}", userId);

            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }

            var streaks = await _streakRepo.GetStreakCreatedByUserIdAsync(userId);
            return streaks.Count > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<GetStreakResponse>>(streaks))
                : ResponseHelper.ToUnprocessable("No streaks found");
        }
        
        public async Task<ServiceResult> GetStreaksByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("About to GetStreaksByUserIdAsync for {userId}", userId);

            // validate userId
            var user = await _userRepo.GetUserAsync(userId);
            if (user == null)
            {
                return ResponseHelper.ToBadRequest("User does not exist");
            }

            var streaks = await _streakRepo.GetStreakParticipationByUserIdAsync(userId);
            return streaks.Count > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<GetStreakResponse>>(streaks.Select(s => s.Streak)).ToList())
                : ResponseHelper.ToUnprocessable("No streaks found");
        }

    }
}
