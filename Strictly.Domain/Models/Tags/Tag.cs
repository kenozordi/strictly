using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Tags
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
    }
}
