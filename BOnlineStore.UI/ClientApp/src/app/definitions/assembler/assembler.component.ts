import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { AssemblerService } from './assembler.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'assembler',
  templateUrl: './assembler.component.html',
  styleUrls: ['./assembler.component.scss'],
})
export class AssemblerComponent extends BaseDefinitionsOnGridComponent {
  public assemblerDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _assemblerService: AssemblerService
  ) {
    super(
      _translate,
      'ASSEMBLER', //Pdf, excel dosya adı
      'ASSEMBLER', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.assemblerDataSource = _assemblerService.getDataSource();
  }
}
