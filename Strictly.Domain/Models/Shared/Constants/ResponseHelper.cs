using Strictly.Domain.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Strictly.Domain.Models.Shared.Constants
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
        public static ServiceResult ToSuccess(object data, string? description = null)
        {
            return new ServiceResult() {
                IsSuccess = true,
                Data = new BaseResponse<object>()
                {
                    Code = Success.Code,
                    Description = description ?? Success.Description,
                    Data = data
                }
            };
        }

        /// <summary>
        /// Return an empty/not found response
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ServiceResult ToEmpty(string? description = null)
        {
            return new ServiceResult()
            {
                IsSuccess = false,
                Error = new CustomProblemDetails()
                {
                    Code = NotFound.Code,
                    Title = NotFound.Description,
                    Detail = description
                }
            };
        }

        /// <summary>
        /// Return an unprocessable entity response
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ServiceResult ToUnprocessable(string? description = null)
        {
            return new ServiceResult()
            {
                IsSuccess = false,
                Error = new CustomProblemDetails()
                {
                    Code = UnprocessableEntity.Code,
                    Title = UnprocessableEntity.Description,
                    Detail = description
                }
            };
        }

        /// <summary>
        /// Return a bad request response
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ServiceResult ToBadRequest(string? description = null)
        {
            return new ServiceResult()
            {
                IsSuccess = false,
                Error = new CustomProblemDetails()
                {
                    Code = FailedValidation.Code,
                    Title = FailedValidation.Description,
                    Detail = description
                }
            };
        }
    }
}
