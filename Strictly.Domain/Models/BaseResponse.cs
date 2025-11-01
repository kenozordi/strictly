using Strictly.Domain.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Domain.Models
{
    public class BaseResponse<T> where T : class
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public T? Data { get; set; }

        /// <summary>
        /// Return a successful response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public BaseResponse<T> Success(T data, string? description = null)
        {
            Code = ResponseCode.Success.Code;
            Description = description ?? ResponseCode.Success.Description;
            Data = data;
            return this;
        }

        /// <summary>
        /// Return a failed response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public BaseResponse<T> Failed(string description, string? code = null)
        {
            Code = code ?? ResponseCode.UnprocessableEntity.Code;
            Description = description;
            Data = null;
            return this;
        }

        /// <summary>
        /// Return an empty/not found response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public BaseResponse<T> Empty(string? description = null)
        {
            Code = ResponseCode.NotFound.Code;
            Description = description ?? ResponseCode.NotFound.Description;
            Data = default;
            return this;
        }
    }
}
