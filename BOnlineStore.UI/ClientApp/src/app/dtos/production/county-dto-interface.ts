import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface CountyDto extends EntityDto {
  code: string;
  name: string;
  cityId?: string;
}
