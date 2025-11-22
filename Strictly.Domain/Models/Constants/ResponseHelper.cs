using Strictly.Domain.Models.Enum;
using Strictly.Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Strictly.Domain.Models.Constants
{
    public static class ResponseHelper
    {
        public static (ResponseCode Code, string Description) Success = (ResponseCode.Success, "Request Processed Successfully");
        public static (ResponseCode Code, string Description) FailedValidation = (ResponseCode.FailedValidation, "One or more validations failed");
        public static (ResponseCode Code, string Description) UnprocessableEntity = (ResponseCode.UnprocessableEntity, "This request cannot be processed");
        public static (ResponseCode Code, string Description) NotFound = (ResponseCode.NotFound, "No records found");


        /// <summary>
        /// Return a successful response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static BaseResponse<object> ToSuccess(object data, string? description = null)
        {
            return new BaseResponse<object>() {
                Code = ResponseCode.Success,
                Description = description ?? Success.Description,
                Data = data
            };
        }

        /// <summary>
        /// Return an empty/not found response
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static CustomProblemDetails ToEmpty(string? description = null)
        {
            return new CustomProblemDetails()
            {
                Code = NotFound.Code,
                Title = NotFound.Description,
                Detail = description,
                Status = (int)HttpStatusCode.NotFound
            };
        }
    }
}
