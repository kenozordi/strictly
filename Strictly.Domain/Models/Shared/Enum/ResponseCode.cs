using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Shared.Enum
{
    public enum ResponseCode
    {
        Success,
        FailedValidation,
        UnprocessableEntity,
        NotFound
    }
}
