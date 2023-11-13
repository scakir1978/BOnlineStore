using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Dtos.WorkOrder;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;
using MongoDB.Driver;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class WorkOrderService : Service<WorkOrder, WorkOrderDto, WorkOrderCreateDto, WorkOrderUpdateDto>, IWorkOrderService
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly IFormulaService _formulaService;


        public WorkOrderService(
            IWorkOrderRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<WorkOrder> validator,
            IFormulaRepository formulaRepository,
            IFormulaService formulaService) : base(repository, mapper, stringLocalizer, validator)
        {
            _formulaRepository = formulaRepository;
            _formulaService = formulaService;
        }

        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        public async Task<List<WorkOrderProductionListDto>> CalculateProductionList(string workOrderId)
        {
            var workOrderProductionList = new List<WorkOrderProductionListDto>();

            //İş emri id si ile iş emrinin detaylarına ulaşılıyor.
            var workOrder = await GetByIdAsync(workOrderId);

            //İş emrindeki modele ait formüller sadece parasal maliyette çıksın denilenler hariç listeleniyor.
            var formulaList = _formulaRepository.Load(x => x.ModelId == workOrder.ModelId &&
                                                      x.FormulaSort != Shared.FormulaSortEnum.FormulaSort.Cost)
                                                .ToList();

            foreach (var formula in formulaList)
            {
                var workOrderProductionItem = new WorkOrderProductionListDto();

                workOrderProductionItem.FormulId = formula.Id;
                workOrderProductionItem.FormulName = formula.Name;
                workOrderProductionItem.RawMaterialId = formula.RawMaterialId;
                workOrderProductionItem.RawMaterialName = ""; //Definitions servisinde alınması gerekiyor.
                workOrderProductionItem.Amount = formula.UsageAmount;
                workOrderProductionItem.ProductionMeasure = await CalculateFormula(formula.FormulaDetails, workOrder.Width1, workOrder.Width2, workOrder.Width3, workOrder.Height);

            }

            return workOrderProductionList;

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
