using Microsoft.AspNetCore.Mvc;
using Strictly.Domain.Models;

namespace Strictly.Api.Factories
{
    public static class ResponseFactory
    {
        public static IActionResult ToObjectResult(BaseResponse baseResponse)
        {
            return baseResponse.Code switch
            {
                Domain.Models.Enum.ResponseCode.Success => new OkObjectResult(baseResponse),
                Domain.Models.Enum.ResponseCode.FailedValidation => new BadRequestObjectResult(baseResponse),
                Domain.Models.Enum.ResponseCode.UnprocessableEntity => new UnprocessableEntityObjectResult(baseResponse),
                Domain.Models.Enum.ResponseCode.NotFound => new NotFoundObjectResult(baseResponse),
                _ => new OkObjectResult(baseResponse),
            };
        }

        // next: return customproblem details for bad requests
    }
}
