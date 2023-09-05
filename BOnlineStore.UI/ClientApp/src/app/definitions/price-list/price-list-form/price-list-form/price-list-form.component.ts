import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PriceListMaster } from '../../models/price-list-form-model';

@Component({
  selector: 'app-price-list-form',
  templateUrl: './price-list-form.component.html',
  styleUrls: ['./price-list-form.component.scss'],
})
export class PriceListFormComponent {
  @Input() priceListMaster: PriceListMaster;
  @Output() closeForm = new EventEmitter<any>();

  onSubmit() {
    this.closeForm.emit();
  }
}
