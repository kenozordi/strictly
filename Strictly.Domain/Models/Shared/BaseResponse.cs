using Strictly.Domain.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Shared
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
