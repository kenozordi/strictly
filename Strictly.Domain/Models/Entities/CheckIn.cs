using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Entities
{
    public class CheckIn : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid StreakId { get; set; }
        public Streak Streak { get; set; }
    }
}
