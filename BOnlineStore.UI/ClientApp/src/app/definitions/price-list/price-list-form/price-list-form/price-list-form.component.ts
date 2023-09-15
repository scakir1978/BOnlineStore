import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PriceListMaster } from '../../models/price-list-form-model';
import CustomStore from 'devextreme/data/custom_store';
import { PriceListService } from '../../price-list.service';

@Component({
  selector: 'app-price-list-form',
  templateUrl: './price-list-form.component.html',
  styleUrls: ['./price-list-form.component.scss'],
})
export class PriceListFormComponent {
  @Input() priceListMaster: PriceListMaster;
  @Output() closeForm = new EventEmitter<any>();

  public colorDataSource: CustomStore;

  /**
   *
   */
  constructor(private _priceListService: PriceListService) {
    this.colorDataSource = _priceListService.getColorDataSource();
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
}
