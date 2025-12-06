using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns.CheckIntoStreak
{
    public class CheckInRequest
    {
        public Guid CheckInId { get; set; }
        public Guid UserId { get; set; }
    }
}
