import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { DistrictService } from './district.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.scss'],
})
export class DistrictComponent extends BaseDefinitionsOnGridComponent {
  public districtDataSource: DataSource;
  public cityDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _districtService: DistrictService
  ) {
    super(
      _translate,
      'DISTRICT', //Pdf, excel dosya adı
      'DISTRICT', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.districtDataSource = _districtService.getDataSource();
    this.cityDataSource = _districtService.getCityDataSource();
  }
}
