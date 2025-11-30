using AutoMapper;
using Strictly.Application.Streaks;
using Strictly.Application.Users;
using Strictly.Domain.Models.CheckIns;
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

        public async Task<ServiceResult> GetCheckInHistory(Guid streakId)
        {
            // validate streakId

            var checkInHistory = await _checkInRepo.GetCheckInHistory(streakId);
            return checkInHistory.Count > 0
                ? ResponseHelper.ToSuccess(_mapper.Map<List<CheckIn>>(checkInHistory))
                : ResponseHelper.ToUnprocessable("No check in to see here");
        }

    }
}
