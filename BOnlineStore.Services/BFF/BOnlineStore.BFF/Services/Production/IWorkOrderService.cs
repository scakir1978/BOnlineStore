using BOnlineStore.BFF.Api.Dtos;
using BOnlineStore.Services.BFF.Api.Dtos;

namespace BOnlineStore.BFF.Api.Services.Production
{
    public interface IWorkOrderService
    {
        /// <summary>
        /// İş emrinin üretiminde kullanılacak malzeme listesini hesaplar.
        /// </summary>
        /// <param name="workOrderId">Malzeme listesi hesaplanacak iş emri id</param>
        /// <returns></returns>
        Task<WorkOrderFormFrontEndDto> CalculateProductionList(string workOrderId);
    }
}
