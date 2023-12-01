using BOnlineStore.Services.BFF.Api.Dtos;

namespace BOnlineStore.BFF.Api.Dtos.Production.WorkOrder
{
    public class WorkOrderFormDto
    {
        public WorkOrderDto? WorkOrder { get; set; }
        public List<WorkOrderProductionListDto>? WorkOrderProductionList { get; set; }
    }
}
