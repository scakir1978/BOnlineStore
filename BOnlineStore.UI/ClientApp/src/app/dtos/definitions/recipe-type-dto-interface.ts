import { FormName } from '../../enums/form-name.enum';
import { RawMaterialIdDto } from './raw-material-id-dto-interface';
import { GlassRawMaterialIdDto } from './raw-material-id-dto-interface';
import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface RecipeTypeDto extends EntityDto {
  code: string;
  name: string;
  formName: FormName | null;
  thisRecipeHasPanel: boolean | null;
  rawMaterialIds: RawMaterialIdDto[] | null;
  panelRawMaterialIds: RawMaterialIdDto[] | null;
  glassRawMaterialIds: GlassRawMaterialIdDto[] | null;
  panelGlassRawMaterialIds: GlassRawMaterialIdDto[] | null;
}
