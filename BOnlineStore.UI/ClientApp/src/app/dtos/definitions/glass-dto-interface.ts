import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface GlassDto extends EntityDto {
  code: string;
  name: string;
  glassGroupId: string;
}
