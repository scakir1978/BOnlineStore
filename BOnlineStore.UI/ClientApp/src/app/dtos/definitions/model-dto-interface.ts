import { EntityDto } from './../../base-classes/base-interfaces/entity-dto-interface';

export interface ModelDto extends EntityDto {
  code: string;
  name: string;
  modelGroupId: string;
  sidePanelId: string | null;
  panelId: string | null;
  recipeTypeId: string | null;
  wastageRatio: number | null;
  wastageAmount: number | null;
  workmanshipRatio: number | null;
  workmanshipAmount: number | null;
  picture: string | null;
}
