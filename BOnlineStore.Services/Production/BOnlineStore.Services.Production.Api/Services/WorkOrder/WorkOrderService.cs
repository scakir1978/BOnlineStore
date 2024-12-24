using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Production.Api.Services;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class WorkOrderService : Service<WorkOrder, WorkOrderDto, WorkOrderCreateDto, WorkOrderUpdateDto>, IWorkOrderService
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly IFormulaService _formulaService;
        private readonly IDefinitionsService _definitionsService;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public WorkOrderService(
            IWorkOrderRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<WorkOrder> validator,
            IFormulaRepository formulaRepository,
            IFormulaService formulaService,
            IDefinitionsService definitionsService
            ) : base(repository, mapper, stringLocalizer, validator)
        {
            _stringLocalizer = stringLocalizer;
            _formulaRepository = formulaRepository;
            _formulaService = formulaService;
            _definitionsService = definitionsService;
        }

        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        public async Task<WorkOrderFormDto> CalculateProductionList(string workOrderId)
        {
            var workOrderProductionList = new List<WorkOrderProductionListDto>();

            //İş emri id si ile iş emrinin detaylarına ulaşılıyor.
            var workOrder = await GetByIdAsync(workOrderId);

            //İş emrindeki modele ait tanımlamaları çekmek için istek yapılıyor.
            var definitionsRequestList = new List<DefinitionsRequestDto> { new DefinitionsRequestDto { EntityId = workOrder.ModelId, EntityName = DefinitionsApiEntityNameConstants.Model } };
            var response = await _definitionsService.GetByIdAsync(definitionsRequestList);


            if (response?.FirstOrDefault()?.Entity == null)
                throw new Exception(_stringLocalizer[SharedKeys.WorkOrderModelNotFound]);
            
            var modelDto = JsonConvert.DeserializeObject<ModelDto>(response?.FirstOrDefault()?.Entity?.ToString() ?? "");

            //İş emrindeki modele ait formüller sadece parasal maliyette çıksın denilenler hariç listeleniyor.
            var formulaList = _formulaRepository.Load(x => x.ModelId == workOrder.ModelId &&
                                                      x.FormulaSort != Shared.FormulaSortEnum.FormulaSort.Cost)
                                                .ToList();

            await AddFormulasToProductionList(formulaList, workOrder, workOrderProductionList);

            //Eğer iş emrinde panel bilgisi varsa panel bilgisine ait formüller de listeye ekleniyor.
            if (!string.IsNullOrWhiteSpace(modelDto?.PanelId))
            {
                //İş emrindeki modele ait formüller sadece parasal maliyette çıksın denilenler hariç listeleniyor.
                var formulaListPanel = _formulaRepository.Load(x => x.PanelId == modelDto.PanelId &&
                                                          x.FormulaSort != Shared.FormulaSortEnum.FormulaSort.Cost)
                                                    .ToList();

                await AddFormulasToProductionList(formulaListPanel, workOrder, workOrderProductionList, true, false);
            }

            //Eğer iş emrinde yan panel bilgisi varsa yan panel bilgisine ait formüller de listeye ekleniyor.
            if (!string.IsNullOrWhiteSpace(modelDto?.SidePanelId))
            {
                //İş emrindeki modele ait formüller sadece parasal maliyette çıksın denilenler hariç listeleniyor.
                var formulaListPanel = _formulaRepository.Load(x => x.PanelId == modelDto.SidePanelId &&
                                                          x.FormulaSort != Shared.FormulaSortEnum.FormulaSort.Cost)
                                                    .ToList();

                await AddFormulasToProductionList(formulaListPanel, workOrder, workOrderProductionList, false, true);
            }

            return new WorkOrderFormDto(workOrder, workOrderProductionList);

        }

        /// <summary>
        /// Hesaplanan formülleri üretim listesine ekler.
        /// </summary>
        /// <param name="formulaList">Hesaplanacak formül listesi</param>
        /// <param name="workOrder">İş emri</param>
        /// <param name="workOrderProductionList">Formüllerin hesaplanarak eklendiği üretim listesi</param>
        /// <param name="isPanel">Paneller ile ilgili bir hesaplama yapılıyorsa true olur.</param>
        /// <param name="isSidePanel">Yan paneller ile ilgili bir hesaplama yapılıyorsa true olur.</param>
        /// <returns></returns>
        private async Task AddFormulasToProductionList(List<Formula> formulaList, WorkOrderDto workOrder,
                                                       List<WorkOrderProductionListDto> workOrderProductionList,
                                                       bool isPanel = false, bool isSidePanel = false)
        {
            decimal? width1 = workOrder.Width1;
            decimal? width2 = workOrder.Width2;
            decimal? width3 = workOrder.Width3;
            decimal? height = workOrder.Height;
            if (isPanel)
            {
                width1 = workOrder.PanelWidth;
                height = workOrder.PanelHeight;
            }
            if (isSidePanel)
            {
                width1 = workOrder.SidePanelWidth;
                height = workOrder.SidePanelHeight;
            }

            foreach (var formula in formulaList)
            {
                var workOrderProductionItem = new WorkOrderProductionListDto
                {
                    FormulId = formula.Id,
                    FormulName = formula.Name,
                    RawMaterialId = formula.RawMaterialId,
                    Amount = formula.UsageAmount,
                    ProductionMeasure = await CalculateFormula(formula.FormulaDetails, width1, width2, width3, height),
                    IsPanel = isPanel,
                    IsSidePanel = isSidePanel
                };

                workOrderProductionList.Add(workOrderProductionItem);
            }
        }

        /// <summary>
        /// Modele ait bir formulü hesaplayıp sonucunu geri döndürür.
        /// </summary>
        /// <param name="formulaDetails">Hesaplanacak formülün nasıl formülünü içiren liste</param>
        /// <param name="width1">Formülde kullanılan en-1 bilgisi</param>
        /// <param name="width2">Formülde kullanılan en-2 bilgisi</param>
        /// <param name="width3">Formülde kullanılan en-3 bilgisi</param>
        /// <param name="height">Formülde kullanılan yükseklik bilgisi</param>
        /// <returns></returns>
        public async Task<decimal> CalculateFormula(List<FormulaDetail>? formulaDetails, decimal? width1, decimal? width2, decimal? width3, decimal? height)
        {
            return await _formulaService.ExecuteFormula(formulaDetails, width1, width2, width3, height);
        }


    }
}
