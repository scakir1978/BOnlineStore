using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BOnlineStore.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Başka servislere get istekleri yapmak için kullanılır.
        /// </summary>
        /// <param name="_client">HttpClient nesnesinde extension olarak gözükmesi için kullanılır.</param>
        /// <param name="controllerName">İstek atılacak controller ismi</param>
        /// <param name="functionName">İstek atılacak controller üzerinde çağrılacak fonksiyonun adı</param>
        /// <param name="parameters">Get fonksiyonun çağrısında kullanılacak parametreler</param>
        /// <param name="_httpContextAccessor">Token bilgisine ulaşmak için kullanılacak IHttpContextAccessor nesnesi</param>
        /// <param name="_stringLocalizer">Hata mesajlarının key karşılıklarını bulan IStringLocalizer<Language> nesnesi</param>
        /// <param name="urlPrefix">Controller ile fonksiyon adlarının önüne gelecek url prefix değeri. Bir değer atanmaz ise varsayılan olarak "/api" olur.
        /// Url prefix değeri "/" ile başlamalıdır. Base uri "https://production.bonlinestore.com" ifadesi şeklindedir. 
        /// O yüzden sonuna "/api" gibi url prefix değeri gelmelidir.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetParameterizedAsync(
            this HttpClient _client, string controllerName, string? functionName,
            List<QueryParameters> parameters,
            IHttpContextAccessor _httpContextAccessor,
            IStringLocalizer<Language> _stringLocalizer,
            string urlPrefix = GlobalConstants.apiPrefix)
        {
            string url = GenerateUrl(urlPrefix, controllerName, functionName, _stringLocalizer);

            //Access token bilgisine ulaşılır.
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(GlobalConstants.AccessToken);

            //Token diğer servise yapılacak istek için headersa eklenir.
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GlobalConstants.Bearer, accessToken);

            //Parametreler query string ifadesine dönüştürülür.
            var queryString = QueryParametersToQueryString(parameters);

            //Eğer query string
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return await _client.GetAsync($"{url}");
            }

            //Çağrı yapılır.
            return await _client.GetAsync($"{url}?{queryString}");
        }

        /// <summary>
        /// Başka servislere post istekleri yapmak için kullanılır.
        /// </summary>
        /// <param name="_client">HttpClient nesnesinde extension olarak gözükmesi için kullanılır.</param>
        /// <param name="controllerName">İstek atılacak controller ismi</param>
        /// <param name="functionName">İstek atılacak controller üzerinde çağrılacak fonksiyonun adı</param>
        /// <param name="parameters">Get fonksiyonun çağrısında kullanılacak parametreler</param>
        /// <param name="_httpContextAccessor">Token bilgisine ulaşmak için kullanılacak IHttpContextAccessor nesnesi</param>
        /// <param name="_stringLocalizer">Hata mesajlarının key karşılıklarını bulan IStringLocalizer<Language> nesnesi</param>
        /// <param name="urlPrefix">Controller ile fonksiyon adlarının önüne gelecek url prefix değeri. Bir değer atanmaz ise varsayılan olarak "/api" olur.
        /// Url prefix değeri "/" ile başlamalıdır. Base uri "https://production.bonlinestore.com" ifadesi şeklindedir. 
        /// O yüzden sonuna "/api" gibi url prefix değeri gelmelidir.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostParameterizedAsync<T>(
            this HttpClient _client, string controllerName, string? functionName,
            T data,
            IHttpContextAccessor _httpContextAccessor,
            IStringLocalizer<Language> _stringLocalizer,
            string urlPrefix = GlobalConstants.apiPrefix)
        {
            string url = GenerateUrl(urlPrefix, controllerName, functionName, _stringLocalizer);

            //Access token bilgisine ulaşılır.
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(GlobalConstants.AccessToken);

            //Token diğer servise yapılacak istek için headersa eklenir.
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GlobalConstants.Bearer, accessToken);

            //Gelen T tipindeki data, json stringe çervrilir.
            var dataAsString = JsonConvert.SerializeObject(data);

            //Content oluşturulur.
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //çağrı yapılır.
            return await _client.PostAsync(url, content);

        }

        /// <summary>
        /// İstek sonrası dönen sonucu, T tipinde verilen sınıfa dönüştürür.
        /// </summary>
        /// <typeparam name="T">Dönen datanın dönüştürülmek istenen sınıfın tipi</typeparam>
        /// <param name="content">HttpContent nesnesinde extension olarak gözükmesi için kullanılır.</param>
        /// <returns></returns>
        public static async Task<Response<T>> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();

            JObject jsonObject = JObject.Parse(dataAsString);

            T? result = JsonConvert.DeserializeObject<T>(jsonObject["result"]?.ToString() ?? "");

            var response = Response<T>.Success(result, System.Net.HttpStatusCode.OK);

            return response;

        }

        /// <summary>
        /// Liste olarak verilen parametreleri query string ifadesine dönüştürür.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        private static string QueryParametersToQueryString(List<QueryParameters> queryParameters)
        {
            var queryString = "";

            foreach (var queryParameter in queryParameters)
            {
                if (queryString.Length > 0)
                {
                    queryString += "&";
                }

                queryString += queryParameter.ParameterName + "=" + queryParameter.ParameterValue;

            }

            return queryString;
        }

        /// <summary>
        /// İstek atılacak urlyi oluşturur.
        /// </summary>
        /// <param name="urlPrefix">Controller ile fonksiyon adlarının önüne gelecek url prefix değeri. Bir değer atanmaz ise va
        /// <param name="controllerName">İstek atılacak controller ismi</param>
        /// <param name="functionName">İstek atılacak controller üzerinde çağrılacak fonksiyonun adı</param>
        /// <param name="_stringLocalizer">Hata mesajlarının key karşılıklarını bulan IStringLocalizer<Language> nesnesi</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static string GenerateUrl(string urlPrefix, string controllerName, string? functionName, IStringLocalizer<Language> _stringLocalizer)
        {
            //Urlde kullanılacak parametrelerin boş olmaması gerekir.
            if (string.IsNullOrWhiteSpace(urlPrefix))
                throw new ArgumentNullException(nameof(urlPrefix), _stringLocalizer[SharedKeys.UrlPrefixCannotBeNull]);
            if (string.IsNullOrWhiteSpace(controllerName))
                throw new ArgumentNullException(nameof(controllerName), _stringLocalizer[SharedKeys.ControllerNameCannotBeNull]);

            string url = $"{urlPrefix}/{controllerName}/{functionName}";
            if (string.IsNullOrWhiteSpace(functionName)) url = $"{urlPrefix}/{controllerName}";

            return url;
        }
    }

    /// <summary>
    /// Paremetre tanımlarının yapıldığı sınıf.
    /// </summary>
    public class QueryParameters
    {
        public string? ParameterName { get; set; }
        public object? ParameterValue { get; set; }

    }
}
