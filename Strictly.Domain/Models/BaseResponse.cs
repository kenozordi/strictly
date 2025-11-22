using Strictly.Domain.Models.Constants;
using Strictly.Domain.Models.Enum;
using Strictly.Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Strictly.Domain.Models
{
    public class BaseResponse
    {
        public ResponseCode? Code { get; set; }
        public string? Description { get; set; }
    }

    public class BaseResponse<T> : BaseResponse where T : class
    {
        public T? Data { get; set; }
    }
}
