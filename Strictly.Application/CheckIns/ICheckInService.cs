using Strictly.Domain.Constants;
using Strictly.Domain.Models.CheckIns.CheckIntoStreak;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.CheckIns
{
    public interface ICheckInService
    {
        Task<ServiceResult> GetActiveCheckInSchedule(Guid streakId);
        Task<ServiceResult> CreateCheckIn(CreateCheckInRequest checkInRequest);
        Task<ServiceResult> CreateCheckInSchedule(Streak streak);
        Task<ServiceResult> CheckIn(CheckInRequest checkInRequest);
        Task<ServiceResult> GetCheckInForToday(Guid userId);
    }
}
