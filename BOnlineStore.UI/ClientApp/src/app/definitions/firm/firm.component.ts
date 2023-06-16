import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { FirmService } from './firm.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { Column } from 'devextreme/ui/data_grid';

@Component({
  selector: 'firm',
  templateUrl: './firm.component.html',
  styleUrls: ['./firm.component.scss'],
})
export class FirmComponent extends BaseDefinitionsOnGridComponent {
  public firmDataSource: DataSource;
  public firmTypeDataSource: CustomStore;
  public countryDataSource: CustomStore;
  public regionDataSource: CustomStore;
  public cityDataSource: CustomStore;
  public countyDataSource: CustomStore;
  public districtDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _firmService: FirmService
  ) {
    super(
      _translate,
      'FIRM', //Pdf, excel dosya adı
      'FIRM', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.firmDataSource = _firmService.getDataSource();
    this.firmTypeDataSource = _firmService.getFirmTypeDataSource();
    this.countryDataSource = _firmService.getCountryDataSource();
    this.regionDataSource = _firmService.getRegionDataSource();
    this.cityDataSource = _firmService.getCityDataSource();
    this.countyDataSource = _firmService.getCountyDataSource();
    this.districtDataSource = _firmService.getDistrictDataSource();

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
