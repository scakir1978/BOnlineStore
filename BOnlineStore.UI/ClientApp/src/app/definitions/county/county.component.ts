import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { CountyService } from './county.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'county',
  templateUrl: './county.component.html',
  styleUrls: ['./county.component.scss'],
})
export class CountyComponent extends BaseDefinitionsOnGridComponent {
  public countyDataSource: DataSource;
  public cityDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _countyService: CountyService
  ) {
    super(
      _translate,
      'COUNTY', //Pdf, excel dosya adı
      'COUNTY', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.countyDataSource = _countyService.getDataSource();
    this.cityDataSource = _countyService.getCityDataSource();
  }
}
