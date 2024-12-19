import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';
import { CountryDto } from './country-dto-interface';
import { CityDto } from './city-dto-interface';
import { CountyDto } from './county-dto-interface';
import { DistrictDto } from './district-dto-interface';

export interface DeliveryAdressDto extends EntityDto {
  code?: string;
  name?: string;
  customerName?: string;
  countryId?: string;
  country: CountryDto;
  cityId?: string;
  city: CityDto;
  countyId?: string;
  county: CountyDto;
  districtId?: string;
  district: DistrictDto;
  adress?: string;
  postalCode?: string;
  cellPhoneNumber?: string;
  telephoneNumber?: string;
  eMail?: string;
}
