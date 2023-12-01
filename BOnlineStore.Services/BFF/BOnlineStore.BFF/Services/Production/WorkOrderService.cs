using BOnlineStore.BFF.Api.Dtos.Production.WorkOrder;
using BOnlineStore.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace BOnlineStore.BFF.Api.Services.Production
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkOrderService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<WorkOrderFormDto> CalculateProductionList(string workOrderId)
        {
            //Buraya isteğin üzerinde gelen token bilgisine ulaşılır.
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            //Token diğer servise yapılacak istek için headersa eklenir.
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //Çağrı yapılır.
            var response = await _client.GetAsync($"/api/CalculateProductionList?workOrderId={workOrderId}");

            //Geri dönen bilgi dto nesnesine dönüştürülür.
            var workOrderFormDto = await response.Content.ReadAsJsonAsync<WorkOrderFormDto>();

            return workOrderFormDto;
        }
    }
}
