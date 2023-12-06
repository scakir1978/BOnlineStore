using BOnlineStore.BFF.Api.Dtos.Production.WorkOrder;
using BOnlineStore.Localization;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Localization;
using System.Net.Http.Headers;

namespace BOnlineStore.BFF.Api.Services.Production
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public WorkOrderService(HttpClient client, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        public async Task<WorkOrderFormDto> CalculateProductionList(string workOrderId)
        {
            var parameters = new List<QueryParameters>();
            parameters.Add(new QueryParameters
            {
                ParameterName = ProductionApiControllerFuctionsParemetersConstants.WorkOrderId,
                ParameterValue = workOrderId
            });

            var response = await _client.GetParameterizedAsync(ProductionApiControllerConstants.WorkOrder,
                                                         ProductionApiControllerFuctionsConstants.CalculateProductionList,
                                                         parameters, _httpContextAccessor, _stringLocalizer);

            //Geri dönen bilgi dto nesnesine dönüştürülür.
            var workOrderFormResponse = await response.Content.ReadAsJsonAsync<WorkOrderFormDto>();

            return workOrderFormResponse.Result;
        }
    }
}
