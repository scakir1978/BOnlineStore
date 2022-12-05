import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ColorGroupService } from './color-group.service';
import { Component } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'color-group',
  templateUrl: './color-group.component.html',
  styleUrls: ['./color-group.component.scss'],
})
export class ColorGroupComponent extends BaseDefinitionsOnGridComponent {
  public colorGroupDataSource: DataSource;

  constructor(
    public _translate: TranslateService,
    public _coreConfigService: CoreConfigService,
    private _colorGroupService: ColorGroupService
  ) {
    super(
      _translate,
      _coreConfigService,
      'KEYS.COLORGROUPS', //Pdf, excel dosya adı
      'KEYS.COLORGROUPS', //breadCrump için kullanılacak componenet keyi
      'KEYS.DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.colorGroupDataSource = _colorGroupService.getDataSource();
  }
}
