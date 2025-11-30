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
        Task<ServiceResult> GetCheckInHistory(Guid streakId);
        //Task<ServiceResult> GetCheckInHistory(Guid streakId, Guid UserId);
    }
}
