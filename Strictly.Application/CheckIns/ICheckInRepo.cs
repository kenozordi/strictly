using Strictly.Domain.Models.CheckIns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.CheckIns
{
    public interface ICheckInRepo
    {
        Task<List<CheckIn>> GetActiveCheckInSchedule(Guid streakId);
        Task<int> CreateCheckIn(CheckIn checkIn);
        Task<int> UpdateCheckIn(CheckIn checkIn);
        Task<CheckIn?> GetCheckIn(Guid checkInId);
        Task<List<CheckIn>> GetCheckInForDate(Guid userId, DateTime date);
    }
}
