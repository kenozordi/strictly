using Strictly.Domain.Enum;
using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns
{
    public class CheckIn : BaseEntity
    {
        public DateTime DueDate { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public Guid StreakId { get; set; }
        public Guid UserId { get; set; }
        public CheckInStatus Status { get; set; } = CheckInStatus.Pending;
        public Streak Streak { get; set; }
    }
}
