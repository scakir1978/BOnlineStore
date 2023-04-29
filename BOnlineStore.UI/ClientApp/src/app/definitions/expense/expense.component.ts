import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ExpenseService } from './expense.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss'],
})
export class ExpenseComponent extends BaseDefinitionsOnGridComponent {
  public expenseDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _expenseService: ExpenseService
  ) {
    super(
      _translate,
      'EXPENSE', //Pdf, excel dosya adı
      'EXPENSE', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.expenseDataSource = _expenseService.getDataSource();
  }
}
