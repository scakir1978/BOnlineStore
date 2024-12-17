import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { DeliveryAdressService } from './delivery-adress.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { Column } from 'devextreme/ui/data_grid';

@Component({
  selector: 'delivery-adress',
  templateUrl: './delivery-adress.component.html',
  styleUrls: ['./delivery-adress.component.scss'],
})
export class DeliveryAdressComponent extends BaseDefinitionsOnGridComponent {
  public deliveryAdressDataSource: DataSource;
  public countryDataSource: CustomStore;
  public regionDataSource: CustomStore;
  public cityDataSource: CustomStore;
  public countyDataSource: CustomStore;
  public districtDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _deliveryAdressService: DeliveryAdressService
  ) {
    super(
      _translate,
      'DELIVERYADRESS', //Pdf, excel dosya adı
      'DELIVERYADRESS', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.deliveryAdressDataSource = _deliveryAdressService.getDataSource();

    this.countryDataSource = _deliveryAdressService.getCountryDataSource();
    this.regionDataSource = _deliveryAdressService.getRegionDataSource();
    this.cityDataSource = _deliveryAdressService.getCityDataSource();
    this.countyDataSource = _deliveryAdressService.getCountyDataSource();
    this.districtDataSource = _deliveryAdressService.getDistrictDataSource();

    this.getFilteredRegions = this.getFilteredRegions.bind(this);
    this.getFilteredCities = this.getFilteredCities.bind(this);
    this.getFilteredCounties = this.getFilteredCounties.bind(this);
    this.getFilteredDistricts = this.getFilteredDistricts.bind(this);
  }

  getFilteredRegions(options) {
    return {
      store: this.regionDataSource,
      filter: options.data ? ['countryId', '=', options.data.countryId] : null,
    };
  }

  getFilteredCities(options) {
    return {
      store: this.cityDataSource,
      filter: options.data ? ['countryId', '=', options.data.countryId] : null,
    };
  }

  getFilteredCounties(options) {
    return {
      store: this.countyDataSource,
      filter: options.data ? ['cityId', '=', options.data.cityId] : null,
    };
  }

  getFilteredDistricts(options) {
    return {
      store: this.districtDataSource,
      filter: options.data ? ['countyId', '=', options.data.countyId] : null,
    };
  }

  setCountryValue(this: Column, newData: any, value: any, currentRowData: any) {
    newData.regionId = null;
    newData.cityId = null;
    this.defaultSetCellValue(newData, value, currentRowData);
  }

  setCityValue(this: Column, newData: any, value: any, currentRowData: any) {
    newData.countyId = null;
    newData.districtId = null;
    this.defaultSetCellValue(newData, value, currentRowData);
  }

  setCountyValue(this: Column, newData: any, value: any, currentRowData: any) {
    newData.districtId = null;
    this.defaultSetCellValue(newData, value, currentRowData);
  }
}
