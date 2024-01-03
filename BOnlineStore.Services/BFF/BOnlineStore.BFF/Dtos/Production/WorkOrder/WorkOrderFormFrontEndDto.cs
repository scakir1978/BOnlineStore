using BOnlineStore.Services.BFF.Api.Dtos;

namespace BOnlineStore.BFF.Api.Dtos
{
    public class WorkOrderFormFrontEndDto
    {
        public WorkOrderDto? WorkOrder { get; set; }
        public List<WorkOrderProductionListDto>? RawMaterials { get; set; }
        public List<WorkOrderProductionListDto>? PanelRawMaterials { get; set; }
        public List<GlassRawMaterialDto>? GlassRawMaterials { get; set; }
        public List<GlassRawMaterialDto>? PanelGlassRawMaterials { get; set; }
        public RecipeTypeDto? RecipeType { get; set; }

    }
}
