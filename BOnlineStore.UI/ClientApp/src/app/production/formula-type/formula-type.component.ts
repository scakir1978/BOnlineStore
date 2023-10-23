import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { FormulaTypeService } from './formula-type.service';
import { Component } from '@angular/core';
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
    public override _translate: TranslateService,
    private _formulaTypeService: FormulaTypeService
  ) {
    super(
      _translate,
      'FORMULATYPES', //Pdf, excel dosya adı
      'FORMULATYPES', //breadCrump için kullanılacak componenet keyi
      'PRODUCTION' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.formulaTypeDataSource = _formulaTypeService.getDataSource();
  }

  onInitNewRow(e) {
    e.data.isVariable1Required = false;
    e.data.isVariable2Required = false;
    e.data.isVariable3Required = false;
    e.data.isVariable4Required = false;
    e.data.isConstant1Required = false;
    e.data.isConstant2Required = false;
  }
}
