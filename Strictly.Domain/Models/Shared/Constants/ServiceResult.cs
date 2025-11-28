using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Shared.Constants
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public BaseResponse? Data { get; set; }
        public CustomProblemDetails? Error { get; set; }
    }
}
