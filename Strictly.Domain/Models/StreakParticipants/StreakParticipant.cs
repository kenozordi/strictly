using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.StreakParticipants
{
    public class StreakParticipant : BaseEntity
    {
        public Guid StreakId { get; set; }
        public Guid UserId { get; set; }

        public Streak Streak { get; set; }
        public User User { get; set; }
    }
}
