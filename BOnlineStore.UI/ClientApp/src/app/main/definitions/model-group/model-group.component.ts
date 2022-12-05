import { BaseDefinitionsOnGridComponent } from './../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ModelGroupService } from './model-group.service';
import { Component } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'model-group',
  templateUrl: './model-group.component.html',
  styleUrls: ['./model-group.component.scss'],
})
export class ModelGroupComponent extends BaseDefinitionsOnGridComponent {
  public modelGroupDataSource: DataSource;

  constructor(
    public _translate: TranslateService,
    public _coreConfigService: CoreConfigService,
    private _modelGroupService: ModelGroupService
  ) {
    super(
      _translate,
      _coreConfigService,
      'KEYS.MODELGROUPS', //Pdf, excel dosya adı
      'KEYS.MODELGROUPS', //breadCrump için kullanılacak componenet keyi
      'KEYS.DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.modelGroupDataSource = _modelGroupService.getDataSource();
  }
}
