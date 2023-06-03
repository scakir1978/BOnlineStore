import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { CityService } from './city.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { Column } from 'devextreme/ui/data_grid';

@Component({
  selector: 'city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss'],
})
export class CityComponent extends BaseDefinitionsOnGridComponent {
  public cityDataSource: DataSource;
  public regionDataSource: CustomStore;
  public countryDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _cityService: CityService
  ) {
    super(
      _translate,
      'CITY', //Pdf, excel dosya adı
      'CITY', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.cityDataSource = _cityService.getDataSource();
    this.regionDataSource = _cityService.getRegionDataSource();
    this.countryDataSource = _cityService.getCountryDataSource();
    this.getFilteredRegions = this.getFilteredRegions.bind(this);
  }

  getFilteredRegions(options) {
    return {
      store: this.regionDataSource,
      filter: options.data ? ['countryId', '=', options.data.countryId] : null,
    };
  }

  setCountryValue(this: Column, newData: any, value: any, currentRowData: any) {
    newData.countryId = null;
    this.defaultSetCellValue(newData, value, currentRowData);
  }

  onEditorPreparing(e) {
    if (e.parentType === 'dataRow' && e.dataField === 'CityID') {
      e.editorOptions.disabled = typeof e.row.data.StateID !== 'number';
    }
  }
}
