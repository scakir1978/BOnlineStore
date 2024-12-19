using BOnlineStore.BFF.Api.Dtos;
using BOnlineStore.BFF.Api.Services.Definitions;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.BFF.Api.Dtos;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Extensions;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
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
        public async Task<WorkOrderFormFrontEndDto> CalculateProductionList(string workOrderId)
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

            //İş emri datası null gelirse exception fırlatılır.
            if (workOrderFormResponse.Result == null)
                throw new ArgumentNullException(nameof(workOrderFormResponse.Result), _stringLocalizer[SharedKeys.WorkOrderFormCannotBeNull]);

            var workOrderForm = workOrderFormResponse.Result;

            //İş emrindeki tanımlama dataları, DefinitionsService üzeriden çekilir.
            List<DefinitionsResponseDto> responseDefinitionsEntities = await GetByIdFromDefinitionsServiceAsync(workOrderForm);

            //Çekilen veriler, iş emri formundaki dtolara atanır.
            FillWorkOrderDefinitionsDataToDto(workOrderForm, responseDefinitionsEntities);

            if (workOrderForm.WorkOrder?.DeliveryAdress != null)
            {
                List<DefinitionsResponseDto> responseDeliveryAdressDefinitionsEntities = await GetDeliveryAdressDefinitionsServiceAsync(workOrderForm.WorkOrder.DeliveryAdress);

                FillDeliveryAdressDefinitionsDataToDto(workOrderForm.WorkOrder.DeliveryAdress, responseDeliveryAdressDefinitionsEntities);
            }

            //Reçete türü bilgilerine ulaşılır.
            await FillRecipeTypeToDto(workOrderForm);

            //Reçetenin hammadde bilgilerine ulaşılır.
            await FillRawMaterialsToDto(workOrderForm);

            //Yapılan hesaplamalar front ende kullanılmak üzere reçeteye göre hazırlanır.
            return PrepareFormToFrontEnd(workOrderForm);
        }

        /// <summary>
        /// Definitions service üzerinden istenen iş emri formundaki tanımlama bilgilerini dto olarak döner
        /// </summary>
        /// <param name="workOrderForm">Tanımlama bilgilerinin istendiği iş emri formu</param>
        /// <returns></returns>
        private async Task<List<DefinitionsResponseDto>> GetByIdFromDefinitionsServiceAsync(WorkOrderFormDto? workOrderForm)
        {
            var definitionsRequestList = new List<DefinitionsRequestDto>();

            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.FirmId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.FirmId, EntityName = DefinitionsApiEntityNameConstants.Firm });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.DeliveryAdressId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.DeliveryAdressId, EntityName = DefinitionsApiEntityNameConstants.DeliveryAdress });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.ColorId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.ColorId, EntityName = DefinitionsApiEntityNameConstants.Color });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.ModelId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.ModelId, EntityName = DefinitionsApiEntityNameConstants.Model });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.GlassId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.GlassId, EntityName = DefinitionsApiEntityNameConstants.Glass });
            if (!string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.TemplateId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = workOrderForm.WorkOrder.TemplateId, EntityName = DefinitionsApiEntityNameConstants.Template });

            var response = await _definitionsService.GetByIdAsync(definitionsRequestList);
            return response;
        }

        /// <summary>
        /// Definitions service üzerinden istenen sevk adres tablosundaki tanımlama bilgilerini dto olarak döner
        /// </summary>
        /// <param name="deliveryAdress">Sevk adreslerine ait entity</param>
        /// <returns></returns>
        private async Task<List<DefinitionsResponseDto>> GetDeliveryAdressDefinitionsServiceAsync(DeliveryAdressDto deliveryAdress)
        {
            var definitionsRequestList = new List<DefinitionsRequestDto>();

            if (!string.IsNullOrWhiteSpace(deliveryAdress.CountryId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = deliveryAdress.CountryId, EntityName = DefinitionsApiEntityNameConstants.Country });
            if (!string.IsNullOrWhiteSpace(deliveryAdress.CityId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = deliveryAdress.CityId, EntityName = DefinitionsApiEntityNameConstants.City });
            if (!string.IsNullOrWhiteSpace(deliveryAdress.CountyId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = deliveryAdress.CountyId, EntityName = DefinitionsApiEntityNameConstants.County });
            if (!string.IsNullOrWhiteSpace(deliveryAdress.DistrictId))
                definitionsRequestList.Add(new DefinitionsRequestDto { EntityId = deliveryAdress.DistrictId, EntityName = DefinitionsApiEntityNameConstants.District });

            var response = await _definitionsService.GetByIdAsync(definitionsRequestList);
            return response;
        }

        /// <summary>
        /// Tanımlama bilgileri, iş emrindeki dtolara taşınır.
        /// </summary>
        /// <param name="workOrderForm">Dto bilgilerinin atanacağı iş emri formu</param>
        /// <param name="responseDefinitionsEntities">Definitions servisinden dönen tanımlama dto nesnesi</param>
        private void FillWorkOrderDefinitionsDataToDto(WorkOrderFormDto workOrderForm, List<DefinitionsResponseDto> responseDefinitionsEntities)
        {
            foreach (var entityItem in responseDefinitionsEntities)
            {
                switch (entityItem.EntityName)
                {
                    case DefinitionsApiEntityNameConstants.Firm:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.Firm = JsonConvert.DeserializeObject<FirmDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.DeliveryAdress:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.DeliveryAdress = JsonConvert.DeserializeObject<DeliveryAdressDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.Color:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.Color = JsonConvert.DeserializeObject<ColorDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.Model:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.Model = JsonConvert.DeserializeObject<ModelDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.Glass:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.Glass = JsonConvert.DeserializeObject<GlassDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.Template:
                        if (entityItem.Entity != null)
                            workOrderForm.WorkOrder.Template = JsonConvert.DeserializeObject<TemplateDto>(entityItem.Entity.ToString());
                        break;
                }
            }

        }

        /// <summary>
        /// Tanımlama bilgileri, iş emrindeki dtolara taşınır.
        /// </summary>
        /// <param name="deliveryAdress">Dto bilgilerinin atanacağı iş emri formu</param>
        /// <param name="responseDefinitionsEntities">Definitions servisinden dönen tanımlama dto nesnesi</param>
        private void FillDeliveryAdressDefinitionsDataToDto(DeliveryAdressDto deliveryAdress, List<DefinitionsResponseDto> responseDefinitionsEntities)
        {
            foreach (var entityItem in responseDefinitionsEntities)
            {
                switch (entityItem.EntityName)
                {
                    case DefinitionsApiEntityNameConstants.Country:
                        if (entityItem.Entity != null)
                            deliveryAdress.Country = JsonConvert.DeserializeObject<CountryDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.City:
                        if (entityItem.Entity != null)
                            deliveryAdress.City = JsonConvert.DeserializeObject<CityDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.County:
                        if (entityItem.Entity != null)
                            deliveryAdress.County = JsonConvert.DeserializeObject<CountyDto>(entityItem.Entity.ToString());
                        break;
                    case DefinitionsApiEntityNameConstants.District:
                        if (entityItem.Entity != null)
                            deliveryAdress.District = JsonConvert.DeserializeObject<DistrictDto>(entityItem.Entity.ToString());
                        break;
                }
            }
        }

        /// <summary>
        /// Reçece türü bilgisine definitions service üzerinden ulaşılır.
        /// </summary>
        /// <param name="workOrderForm">Reçete türü bilgilerinin atanacağı iş emri formu</param>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task FillRecipeTypeToDto(WorkOrderFormDto workOrderForm)
        {
            if (string.IsNullOrWhiteSpace(workOrderForm.WorkOrder.Model.RecipeTypeId))
                throw new ArgumentNullException(nameof(workOrderForm.WorkOrder.Model.RecipeTypeId), _stringLocalizer[SharedKeys.WorkOrderModelRecipeTypeCannotBeNull]);

            var definitionsRequest = new List<DefinitionsRequestDto>()
            {
                new DefinitionsRequestDto {
                    EntityId = workOrderForm.WorkOrder.Model.RecipeTypeId,
                    EntityName = DefinitionsApiEntityNameConstants.RecipeType
                }
            };

            var response = await _definitionsService.GetByIdAsync(definitionsRequest);

            workOrderForm.RecipeType = JsonConvert.DeserializeObject<RecipeTypeDto>(response.FirstOrDefault().Entity.ToString());

        }

        /// <summary>
        /// Üretilecek hammeddelerin detay bilgilerine Definitions Service üzerinden ulaşılır.
        /// </summary>
        /// <param name="workOrderForm">Reçete türü bilgilerinin atanacağı iş emri formu</param>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task FillRawMaterialsToDto(WorkOrderFormDto workOrderForm)
        {
            if (workOrderForm.WorkOrderProductionList == null)
                throw new ArgumentNullException(nameof(workOrderForm.WorkOrderProductionList), _stringLocalizer[SharedKeys.WorkOrderProductionListCannotBeNull]);

            var rawMaterialIds = workOrderForm.WorkOrderProductionList.Select(x => x.RawMaterialId).ToList();

            var rawMaterialDtoList = await _definitionsService.RawMaterialLoadFromList(rawMaterialIds);

            foreach (var rawMaterialDto in rawMaterialDtoList)
            {
                var productionMaterials = workOrderForm.WorkOrderProductionList.Where(x => x.RawMaterialId == rawMaterialDto.Id).ToList();

                foreach (var productionMaterial in productionMaterials)
                {
                    productionMaterial.RawMaterial = rawMaterialDto;
                }
                
            }

        }

        /// <summary>
        /// Hesaplanan ve hazırlanan iş emri front end tarafında kullanılacak şekile getirilir.
        /// </summary>
        /// <param name="workOrderForm">Front end tarafında kullanıma hazırlanacak olan iş emri dtosu</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private WorkOrderFormFrontEndDto PrepareFormToFrontEnd(WorkOrderFormDto workOrderForm)
        {
            if (workOrderForm.WorkOrderProductionList == null)
                throw new ArgumentNullException(nameof(workOrderForm.WorkOrderProductionList), _stringLocalizer[SharedKeys.WorkOrderProductionListCannotBeNull]);

            var workOrderFormFrontEnd = new WorkOrderFormFrontEndDto();
            workOrderFormFrontEnd.RawMaterials = new List<WorkOrderProductionListDto>();
            workOrderFormFrontEnd.PanelRawMaterials = new List<WorkOrderProductionListDto>();
            workOrderFormFrontEnd.GlassRawMaterials = new List<GlassRawMaterialDto>();
            workOrderFormFrontEnd.PanelGlassRawMaterials = new List<GlassRawMaterialDto>();

            //İş emri master bilgileri taşıyorum.
            workOrderFormFrontEnd.WorkOrder = workOrderForm.WorkOrder;
            //Reçete türü bilgileri taşıyorum.
            workOrderFormFrontEnd.RecipeType = workOrderForm.RecipeType;

            //Üretimde kullanılacak hammaddeleri reçeteye göre sıralı olarak listeye ekliyorum.
            foreach (var recipeRawMaterial in workOrderForm.RecipeType.RawMaterialIds)
            {
                var rawMaterials = workOrderForm.WorkOrderProductionList
                                    .Where(x => x.RawMaterialId.Trim() == recipeRawMaterial.Id.Trim()).ToList();
                                    

                if (rawMaterials != null)
                {
                    workOrderFormFrontEnd.RawMaterials.AddRange(rawMaterials);
                }
            }

            //Üretimde kullanılacak panel hammaddeleri reçeteye göre sıralı olarak listeye ekliyorum.
            foreach (var recipeRawMaterial in workOrderForm.RecipeType.PanelRawMaterialIds)
            {
                var rawMaterials = workOrderForm.WorkOrderProductionList
                                    .Where(x => x.RawMaterialId.Trim() == recipeRawMaterial.Id.Trim()).ToList();

                if (rawMaterials != null)
                {
                    workOrderFormFrontEnd.PanelRawMaterials.AddRange(rawMaterials);
                }
            }

            //Üretimde kullanılacak cam hammadeleri reçeteye göre sıralı olarak listeye ekliyorum
            foreach (var recipeRawMaterial in workOrderForm.RecipeType.GlassRawMaterialIds)
            {
                var glassRawMaterial = new GlassRawMaterialDto();

                glassRawMaterial.WidthMaterial = workOrderForm.WorkOrderProductionList
                                    .FirstOrDefault(x => x.RawMaterialId.Trim() == recipeRawMaterial.WidthMaterialId.Trim());

                glassRawMaterial.LengthMaterial = workOrderForm.WorkOrderProductionList
                                    .FirstOrDefault(x => x.RawMaterialId.Trim() == recipeRawMaterial.LengthMaterialId.Trim());

                if (glassRawMaterial.WidthMaterial != null || glassRawMaterial.LengthMaterial != null)
                {
                    workOrderFormFrontEnd.GlassRawMaterials.Add(glassRawMaterial);
                }
            }

            //Üretimde kullanılacak panel cam hammadeleri reçeteye göre sıralı olarak listeye ekliyorum
            foreach (var recipeRawMaterial in workOrderForm.RecipeType.PanelGlassRawMaterialIds)
            {
                var glassRawMaterial = new GlassRawMaterialDto();

                glassRawMaterial.WidthMaterial = workOrderForm.WorkOrderProductionList
                                    .FirstOrDefault(x => x.RawMaterialId.Trim() == recipeRawMaterial.WidthMaterialId.Trim());

                glassRawMaterial.LengthMaterial = workOrderForm.WorkOrderProductionList
                                    .FirstOrDefault(x => x.RawMaterialId.Trim() == recipeRawMaterial.LengthMaterialId.Trim());

                if (glassRawMaterial.WidthMaterial != null || glassRawMaterial.LengthMaterial != null)
                {
                    workOrderFormFrontEnd.PanelGlassRawMaterials.Add(glassRawMaterial);
                }
            }

            return workOrderFormFrontEnd;
        }
    }
}
