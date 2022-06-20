using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Dtos
{
    public class Response<T>
    {
        public T? Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSucceed { get; private set; }

        public List<Error>? Errors { get; private set; }

        //Static factory methods
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data=data, StatusCode = statusCode, IsSucceed=true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSucceed = true };
        }

        public static Response<T> Fail(List<Error> errors, int statusCode)
        {
            return new Response<T> { Errors = errors,  StatusCode = statusCode, IsSucceed = false };
        }

        public static Response<T> Fail(Error error, int statusCode)
        {
            return new Response<T> { Errors = new List<Error>() { error }, StatusCode = statusCode, IsSucceed = false };
        }


    }
}
