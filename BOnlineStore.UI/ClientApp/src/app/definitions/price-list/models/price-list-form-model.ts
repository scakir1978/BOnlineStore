import {
  IPriceListColorDifference,
  IPriceListDetail,
  IPriceListGlassDifference,
  IPriceListMaster,
  IPriceListMeasurementDifference,
} from './price-list-form-interface';

/** Fiyat listesi master */
export class PriceListMaster implements IPriceListMaster {
  constructor(id: string) {
    this.id = id;
    this.priceListColorDifferences = [];
    this.priceListMeasurementDifferences = [];
    this.priceListGlassDifferences = [];
    this.priceListDetails = [];
  }
  id: string;
  code: string;
  name: string;
  firstDate: string;
  endDate: string;
  priceListMeasurementDifferences: IPriceListMeasurementDifference[];
  priceListGlassDifferences: IPriceListGlassDifference[];
  priceListColorDifferences: IPriceListColorDifference[];
  priceListDetails: IPriceListDetail[];
}

/** Renk farklarından oluşan fiyat bilgileri */
export class PriceListColorDifference implements IPriceListColorDifference {
  constructor(id: string) {
    this.colorId = id;
  }

  colorId: string;
  percentDifference: number;
  currencyDifference: number;
  salePrice: number;
}
