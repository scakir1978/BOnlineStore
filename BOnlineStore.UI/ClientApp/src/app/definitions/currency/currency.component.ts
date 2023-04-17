import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { CurrencyService } from './currency.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.scss'],
})
export class CurrencyComponent extends BaseDefinitionsOnGridComponent {
  public currencyDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _currencyService: CurrencyService
  ) {
    super(
      _translate,
      'CURRENCY', //Pdf, excel dosya adı
      'CURRENCY', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.currencyDataSource = _currencyService.getDataSource();
  }
}
