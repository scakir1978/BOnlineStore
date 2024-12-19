import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface DistrictDto extends EntityDto {
  code?: string;
  name?: string;
  countyId?: string;
}
