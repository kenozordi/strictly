using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Shared.Enum;
using Strictly.Domain.Models.Tags;
using Strictly.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Streaks
{
    public class GetStreakResponse
    {
        public DateTime CreatedAt { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Frequency { get; set; }
        public bool IsActive { get; set; }
    }
}
