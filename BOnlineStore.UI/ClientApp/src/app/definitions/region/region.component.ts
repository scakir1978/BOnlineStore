import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { RegionService } from './region.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.scss'],
})
export class RegionComponent extends BaseDefinitionsOnGridComponent {
  public regionDataSource: DataSource;
  public countryDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _regionService: RegionService
  ) {
    super(
      _translate,
      'REGION', //Pdf, excel dosya adı
      'REGION', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.regionDataSource = _regionService.getDataSource();
    this.countryDataSource = _regionService.getCountryDataSource();
  }
}
