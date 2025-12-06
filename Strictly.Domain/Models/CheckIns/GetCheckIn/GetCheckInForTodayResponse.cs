using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns.GetCheckIn
{
    public class GetCheckInForTodayResponse
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public Guid StreakId { get; set; }
        public string StreakTitle { get; set; }
        public string Status { get; set; }
    }
}
