using Strictly.Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.CheckIns
{
    public class CheckIn : BaseEntity
    {
        public DateTime? DueDate { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public Guid StreakId { get; set; }
        public Guid UserId { get; set; }
        public int Status { get; set; }
    }
}
