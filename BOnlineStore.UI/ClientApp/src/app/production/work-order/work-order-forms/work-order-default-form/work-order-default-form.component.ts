import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WorkOrderFormFrontEndDto } from '../../../../dtos/production/work-order-form-front-end-interface';

@Component({
  selector: 'app-work-order-default-form',
  templateUrl: './work-order-default-form.component.html',
  styleUrls: ['./work-order-default-form.component.scss'],
})
export class WorkOrderDefaultFormComponent {
  @Input() workOrderForm: WorkOrderFormFrontEndDto;
  @Output() closeForm = new EventEmitter<any>();

  onClose() {
    this.closeForm.emit();
  }
}
