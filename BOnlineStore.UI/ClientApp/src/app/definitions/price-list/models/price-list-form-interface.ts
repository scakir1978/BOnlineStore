/** Fiyat listesi renk farkları */
export interface IPriceListColorDifference {
  /** Renk id */
  colorId: string | null;
  /**Yüzdelik fark */
  percentDifference: number | null;
  /**Parasal fark */
  currencyDifference: number | null;
  /**Satış fiyatı */
  salePrice: number | null;
}

/** Fiyat listesi cam türü farkları */
export interface IPriceListGlassDifference {
  /** Cam türü id */
  glassId: string | null;
  /**Yüzdelik fark */
  percentDifference: number | null;
  /**Parasal fark */
  currencyDifference: number | null;
  /**Satış fiyatı */
  salePrice: number | null;
}

/** Fiyat listesi ölçü farkları */
export interface IPriceListMeasurementDifference {
  /** Ölçü */
  measurement: number | null;
  /**Yüzdelik fark */
  percentDifference: number | null;
  /**Parasal fark */
  currencyDifference: number | null;
  /**Satış fiyatı */
  salePrice: number | null;
}

/** Fiyat listesi detay entity. */
export interface IPriceListDetail {
  /** Fiyat listesi id */
  priceListId: string | null;
  /** Model id */
  modelId: string | null;
  /** Renk id */
  colorId: string | null;
  /** Cam türü id */
  glassId: string | null;
  /** İlk en-1  ölçüsü */
  firstWidth01: number | null;
  /** Son en-1  ölçüsü */
  lastWidth01: number | null;
  /** İlk en-2  ölçüsü */
  firstWidth02: number | null;
  /** Son en-2  ölçüsü */
  lastWidth02: number | null;
  /** İlk yükseklik  ölçüsü */
  firstHeight: number | null;
  /** Son yükseklik  ölçüsü */
  lastHeight: number | null;
  /** Satış fiyatı */
  salePrice: number | null;
  /** Döviz kodu id */
  currencyId: string | null;
  /** Ölçü farklarından oluşan fiyat bilgileri */
  priceListMeasurementDifferences: IPriceListMeasurementDifference[] | null;
  /** Cam deseni farklarından oluşan fiyat bilgileri */
  priceListGlassDifferences: IPriceListGlassDifference[] | null;
  /** Renk farklarından oluşan fiyat bilgileri */
  priceListColorDifferences: IPriceListColorDifference[] | null;
}

/** Fiyat listesi master */
export interface IPriceListMaster {
  /** Fiyat listesi id*/
  id: string | null;
  /** Fiyat listesi kodu */
  code: string | null;
  /** Fiyatlistesi açıklaması veya adı */
  name: string | null;
  /** Fiyat listesi geçerlilik süresi başlangıç tarihi */
  firstDate: string | null;
  /** Fiyat listesi geçerlilik süresi bitiş tarihi */
  endDate: string | null;
  /** Ölçü farklarından oluşan fiyat bilgileri */
  priceListMeasurementDifferences: IPriceListMeasurementDifference[] | null;
  /** Cam deseni farklarından oluşan fiyat bilgileri */
  priceListGlassDifferences: IPriceListGlassDifference[] | null;
  /** Renk farklarından oluşan fiyat bilgileri */
  priceListColorDifferences: IPriceListColorDifference[] | null;
  /** Fiyat listesi detayları */
  priceListDetails: IPriceListDetail[] | null;
}
