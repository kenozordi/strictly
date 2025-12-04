using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.Shared.Constants;
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
    }
}
