import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ColorService } from './color.service';
import { Component } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'color',
  templateUrl: './color.component.html',
  styleUrls: ['./color.component.scss'],
})
export class ColorComponent extends BaseDefinitionsOnGridComponent {
  public colorDataSource: DataSource;
  public colorGroupDataSource: DataSource;

  constructor(
    public _translate: TranslateService,
    public _coreConfigService: CoreConfigService,
    private _colorService: ColorService
  ) {
    super(
      _translate,
      _coreConfigService,
      'Color', //Pdf, excel dosya adı
      'KEYS.COLOR', //breadCrump için kullanılacak componenet keyi
      'KEYS.DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.colorDataSource = _colorService.getDataSource();
    this.colorGroupDataSource = _colorService.getColorGroupDataSource();
  }
}
