using BOnlineStore.Shared.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BOnlineStore.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<Response<T>> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();

            JObject jsonObject = JObject.Parse(dataAsString);

            T? result = JsonConvert.DeserializeObject<T>(jsonObject["result"]?.ToString() ?? "");

            var response = Response<T>.Success(result, System.Net.HttpStatusCode.OK);

            return response;

        }
    }
}
