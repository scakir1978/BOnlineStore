using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using System.Linq.Expressions;

namespace BOnlineStore.Services.Production.Api.Services
{
    public interface IWorkOrderService : IService<WorkOrder, WorkOrderDto, WorkOrderCreateDto, WorkOrderUpdateDto>
    {
        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        Task<List<WorkOrderProductionListDto>> CalculateProductionList(string workOrderId);

        /// <summary>
        /// Modele ait bir formulü hesaplayıp sonucunu geri döndürür.
        /// </summary>
        /// <param name="formulaDetails">Hesaplanacak formülün nasıl formülünü içiren liste</param>
        /// <param name="width1">Formülde kullanılan en-1 bilgisi</param>
        /// <param name="width2">Formülde kullanılan en-2 bilgisi</param>
        /// <param name="width3">Formülde kullanılan en-3 bilgisi</param>
        /// <param name="height">Formülde kullanılan yükseklik bilgisi</param>
        /// <returns></returns>
        Task<decimal> CalculateFormula(List<FormulaDetail>? formulaDetails, decimal? width1, decimal? width2, decimal? width3, decimal? height);
    }
}
