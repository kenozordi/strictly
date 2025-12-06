using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns.CreateCheckIn
{
    public class CreateCheckInRequest
    {
        public Guid StreakId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
