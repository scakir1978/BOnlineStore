using BOnlineStore.Services.BFF.Api.Dtos;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    public class WorkOrderFormDto
    {
        public WorkOrderDto? WorkOrder { get; set; }
        public List<WorkOrderProductionListDto>? WorkOrderProductionList { get; set; }
        public RecipeTypeDto? RecipeType { get; set; }
    }
}
