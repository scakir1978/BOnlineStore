import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PriceListMaster } from '../../models/price-list-form-model';
import CustomStore from 'devextreme/data/custom_store';
import { PriceListService } from '../../price-list.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-price-list-form',
  templateUrl: './price-list-form.component.html',
  styleUrls: ['./price-list-form.component.scss'],
})
export class PriceListFormComponent {
  @Input() priceListMaster: PriceListMaster;
  @Output() closeForm = new EventEmitter<any>();

  public colorDataSource: CustomStore;
  public modelDataSource: CustomStore;
  public glassDataSource: CustomStore;
  public currencyDataSource: CustomStore;

  /**
   *
   */
  constructor(
    private _priceListService: PriceListService,
    public _translate: TranslateService
  ) {
    this.colorDataSource = _priceListService.getColorDataSource();
    this.modelDataSource = _priceListService.getModelDataSource();
    this.glassDataSource = _priceListService.getGlassDataSource();
    this.currencyDataSource = _priceListService.getCurrencyDataSource();
  }

  buttonOptions: any = {
    text: 'Kaydet',
    type: 'normal',
    useSubmitBehavior: true,
  };

  onSubmit() {
    this.closeForm.emit(this.priceListMaster);
  }

  onSaved(e: any, data: any) {
    var values = e.component.getDataSource().items();
    data.setValue(values);
  }

  onRowInsertingPriceListMeasurementDifferences(e) {
    var values: any[] = [];
    values = e.component.getDataSource().items();

    const found = values.find((data) => {
      return data.measurement === e.data.measurement;
    });

    if (found) {
      e.cancel = true;
      e.component
        .instance()
        .getController('editing')
        ._fireDataErrorOccurred(this._translate.instant('DUPLICATEKEY'));
    }
  }

  onInitNewRow(e: any) {
    e.data.priceListMeasurementDifferences = [];
    e.data.priceListGlassDifferences = [];
    e.data.priceListColorDifferences = [];
  }
}
