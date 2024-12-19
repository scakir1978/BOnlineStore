import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface CountryDto extends EntityDto {
  code?: string;
  name?: string;
}
