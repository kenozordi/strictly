using Microsoft.AspNetCore.Mvc;
using Strictly.Domain.Constants;
using Strictly.Domain.Enum;
using System.Net;

namespace Strictly.Api.Extensions
{
    public static class ControllerResult
    {
        public static (int HttpStatusCode, object? Response) FormatResponse(this ServiceResult serviceResult)
        {
            switch (serviceResult.IsSuccess)
            {
                case true: 
                    return ((int)HttpStatusCode.OK, serviceResult.Data);
                case false: 
                    return HandleUnsuccessful(serviceResult);
            }
        }

        private static (int HttpStatusCode, object? Response) HandleUnsuccessful(ServiceResult serviceResult)
        {
            switch (serviceResult.Error.Code)
            {
                case ResponseCode.NotFound: 
                    return ((int)HttpStatusCode.NotFound, serviceResult.Error.SetStatus((int)HttpStatusCode.NotFound));
                case ResponseCode.UnprocessableEntity: 
                    return ((int)HttpStatusCode.UnprocessableEntity, serviceResult.Error.SetStatus((int)HttpStatusCode.UnprocessableEntity));
                case ResponseCode.FailedValidation: 
                    return ((int)HttpStatusCode.BadRequest, serviceResult.Error.SetStatus((int)HttpStatusCode.BadRequest));
                default:
                    return ((int)HttpStatusCode.InternalServerError, serviceResult.Error.SetStatus((int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
