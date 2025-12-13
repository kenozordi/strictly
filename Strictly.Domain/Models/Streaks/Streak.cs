using Strictly.Domain.Enum;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.Reminders;
using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Tags;
using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Streaks
{
    public class Streak : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public StreakFrequency Frequency { get; set; } = StreakFrequency.Daily;
        public Guid UserId { get; set; }
        public DateTime? EndDate { get; set; }

        // Relationships
        public User? User { get; set; }
        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
