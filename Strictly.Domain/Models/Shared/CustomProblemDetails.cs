using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Shared
{
    public class CustomProblemDetails
    {
        public string? Type { get; set; }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public ResponseCode? Code { get; set; }

        public CustomProblemDetails SetStatus(int? status)
        {
            this.Status = status;
            return this;
        }
    }
}
