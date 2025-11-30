using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns
{
    public class CheckInRequest
    {
        public Guid StreakId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
