import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface FirmDto extends EntityDto {
  code: string;
  name: string;
  firmTypeId: string | null;
  title: string | null;
  competentPerson: string | null;
  customerRepresentative: string | null;
  taxNumber: string | null;
  taxAdministration: string | null;
  countryId: string | null;
  regionId: string | null;
  cityId: string | null;
  countyId: string | null;
  districtId: string | null;
  adress: string | null;
  postalCode: string | null;
  cellPhoneNumber1: string | null;
  cellPhoneNumber2: string | null;
  telephoneNumber1: string | null;
  telephoneNumber2: string | null;
  telephoneNumber3: string | null;
  faxNumber: string | null;
  eMail: string | null;
  paymentMethod: string | null;
  transactionType: string | null;
  specialCode: string | null;
  priceListId: string | null;
  riskLimit: number | null;
  advanceDiscount1: number | null;
  advanceDiscount2: number | null;
  forwardDiscount: number | null;
  valor: number | null;
  vatRatioForInvoice: boolean | null;
  vatRatio: number | null;
  explanations: string | null;
}
