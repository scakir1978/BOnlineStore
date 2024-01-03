import { RawMaterialDto } from '../definitions/raw-material-dto-interface';
import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface WorkOrderProductionListDto extends EntityDto {
  workOrderId: string | null;
  formulId: string | null;
  formulName: string | null;
  rawMaterialId: string | null;
  rawMaterial: RawMaterialDto | null;
  amount: number | null;
  productionMeasure: number | null;
}
