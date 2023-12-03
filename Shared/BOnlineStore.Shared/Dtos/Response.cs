using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BOnlineStore.Shared.Dtos
{
    public class Response<T>
    {
        public T? Result { get; private set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSucceed { get; private set; }

        public List<Error>? Errors { get; private set; }

        //Static factory methods
        public static Response<T> Success(T? data, HttpStatusCode statusCode)
        {
            return new Response<T> { Result = data, StatusCode = statusCode, IsSucceed = true };
        }

        public static Response<T> Success(HttpStatusCode statusCode)
        {
            return new Response<T> { Result = default(T), StatusCode = statusCode, IsSucceed = true };
        }

        public static Response<T> Fail(List<Error> errors, HttpStatusCode statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSucceed = false };
        }

        public static Response<T> Fail(Error error, HttpStatusCode statusCode)
        {
            return new Response<T> { Errors = new List<Error>() { error }, StatusCode = statusCode, IsSucceed = false };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


    }
}
