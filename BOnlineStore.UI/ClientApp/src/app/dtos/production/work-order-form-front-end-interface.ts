import { WorkOrderDto } from './work-order-dto-interface';
import { WorkOrderProductionListDto } from './production-list-dto-interface';
import { GlassRawMaterialDto } from './glass-raw-material-dto-interface';

export interface WorkOrderFormFrontEndDto {
  workOrder: WorkOrderDto | null;
  rawMaterials: WorkOrderProductionListDto[] | null;
  panelRawMaterials: WorkOrderProductionListDto[] | null;
  glassRawMaterials: GlassRawMaterialDto[] | null;
  panelGlassRawMaterials: GlassRawMaterialDto[] | null;
}
