using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models.Constants
{
    public static class ResponseHelper
    {
        public static (string Code, string Description) Success = ("00", "Request Processed Successfully");
        public static (string Code, string Description) FailedValidation = ("01", "One or more validations failed");
        public static (string Code, string Description) UnprocessableEntity = ("92", "This request cannot be processed");
        public static (string Code, string Description) NotFound = ("25", "No records found");
    }
}
