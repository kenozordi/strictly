using Strictly.Domain.Models.Shared;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Users
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation
        public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
    }
}
