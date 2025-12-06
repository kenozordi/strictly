using Strictly.Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Constants
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public BaseResponse? Data { get; set; }
        public CustomProblemDetails? Error { get; set; }
    }
}
