using Strictly.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Entities
{
    public class Streak : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public StreakFrequency Frequency { get; set; } = StreakFrequency.Daily;
        public bool IsActive { get; set; } = true;

        // Relationships
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
