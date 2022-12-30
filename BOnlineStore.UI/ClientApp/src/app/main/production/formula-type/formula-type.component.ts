import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { FormulaTypeService } from './formula-type.service';
import { Component } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'formula-type',
  templateUrl: './formula-type.component.html',
  styleUrls: ['./formula-type.component.scss'],
})
export class FormulaTypeComponent extends BaseDefinitionsOnGridComponent {
  public formulaTypeDataSource: DataSource;

  constructor(
    public _translate: TranslateService,
    public _coreConfigService: CoreConfigService,
    private _formulaTypeService: FormulaTypeService
  ) {
    super(
      _translate,
      _coreConfigService,
      'KEYS.FORMULATYPES', //Pdf, excel dosya adı
      'KEYS.FORMULATYPES', //breadCrump için kullanılacak componenet keyi
      'KEYS.PRODUCTION' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.formulaTypeDataSource = _formulaTypeService.getDataSource();
  }
}
