import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ExchangeRateService } from './exchange-rate.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'exchange-rate',
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.scss'],
})
export class ExchangeRateComponent extends BaseDefinitionsOnGridComponent {
  public exchangeRateDataSource: DataSource;
  public currencyDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _exchangeRateService: ExchangeRateService
  ) {
    super(
      _translate,
      'EXCHANGERATE', //Pdf, excel dosya adı
      'EXCHANGERATE', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.exchangeRateDataSource = _exchangeRateService.getDataSource();
    this.currencyDataSource = _exchangeRateService.getCurrencyDataSource();
  }
}
