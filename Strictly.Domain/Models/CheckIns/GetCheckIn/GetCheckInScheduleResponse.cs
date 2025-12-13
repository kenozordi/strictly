using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns.GetCheckIn
{
    public class GetCheckInScheduleResponse
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
    }
}
