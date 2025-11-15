using Strictly.Domain.Models.Constants;
using Strictly.Domain.Models.Enum;
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

        /// <summary>
        /// Return a successful response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public BaseResponse<T> Success(T data, string? description = null)
        {
            Code = ResponseCode.Success;
            Description = description ?? ResponseHelper.Success.Description;
            Data = data;
            return this;
        }

        /// <summary>
        /// Return a failed response
        /// </summary>
        /// <param name="description"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public BaseResponse<T> Failed(string description, ResponseCode? code = null)
        {
            Code = code ?? ResponseCode.UnprocessableEntity;
            Description = description;
            Data = null;
            return this;
        }

        /// <summary>
        /// Return an empty/not found response
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public BaseResponse<T> Empty(string? description = null)
        {
            Code = ResponseCode.NotFound;
            Description = description ?? ResponseHelper.NotFound.Description;
            Data = default;
            return this;
        }
    }
}
