import { EntityDto } from './../../base-classes/base-interfaces/entity-dto-interface';

export interface ColorDto extends EntityDto {
  code: string;
  name: string;
  colorGroupId: string;
}
