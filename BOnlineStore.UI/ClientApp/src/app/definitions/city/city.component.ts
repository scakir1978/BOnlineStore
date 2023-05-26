import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { CityService } from './city.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss'],
})
export class CityComponent extends BaseDefinitionsOnGridComponent {
  public cityDataSource: DataSource;
  public regionDataSource: CustomStore;

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
  }
}
