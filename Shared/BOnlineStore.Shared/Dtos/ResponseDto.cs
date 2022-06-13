using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T? Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSucceed { get; private set; }

        public List<ErrorDto>? Errors { get; private set; }

        //Static factory methods
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data=data, StatusCode = statusCode, IsSucceed=true };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSucceed = true };
        }

        public static ResponseDto<T> Fail(List<ErrorDto> errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = errors,  StatusCode = statusCode, IsSucceed = false };
        }

        public static ResponseDto<T> Fail(ErrorDto error, int statusCode)
        {
            return new ResponseDto<T> { Errors = new List<ErrorDto>() { error }, StatusCode = statusCode, IsSucceed = false };
        }


    }
}
