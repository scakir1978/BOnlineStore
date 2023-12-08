using BOnlineStore.BFF.Api.Services.Definitions;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.BFF.Api.Dtos;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Extensions;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BOnlineStore.BFF.Api.Services.Production
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IDefinitionsService _definitionsService;

        public WorkOrderService(HttpClient client, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer, IDefinitionsService definitionsService)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
            _definitionsService = definitionsService;
        }

        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        public async Task<WorkOrderFormDto> CalculateProductionList(string workOrderId)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Type? type = executingAssembly.DefinedTypes.Where(x => x.Name == "WorkOrderFormDto").FirstOrDefault();

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

            await FillWorkOrderDefinitionsDataToDto(workOrderFormResponse.Result);

            return workOrderFormResponse.Result;
        }

        private async Task FillWorkOrderDefinitionsDataToDto(WorkOrderFormDto? workOrderForm)
        {
            if (workOrderForm == null) { throw new ArgumentNullException(nameof(workOrderForm), _stringLocalizer[SharedKeys.WorkOrderFormCannotBeNull]); }

            var definitionsRequestList = new List<DefinitionsRequestDto>();

            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.FirmId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.FirmId, EntityName = "Firm" });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.ColorId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.ColorId, EntityName = "Color" });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.ModelId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.ModelId, EntityName = "Model" });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.GlassId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.GlassId, EntityName = "Glass" });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.TemplateId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.TemplateId, EntityName = "Template" });

            var response = await _definitionsService.GetByIdAsync(definitionsRequestList);

        }
    }
}
