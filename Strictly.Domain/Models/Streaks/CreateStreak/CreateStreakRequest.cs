using Strictly.Domain.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Streaks.CreateStreak
{
    public class CreateStreakRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StreakFrequency Frequency { get; set; } = StreakFrequency.Daily;
        public Guid UserId { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
