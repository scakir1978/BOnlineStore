import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface CityDto extends EntityDto {
  code?: string;
  name?: string;
  countryId?: string;
  regionId?: string;
}
