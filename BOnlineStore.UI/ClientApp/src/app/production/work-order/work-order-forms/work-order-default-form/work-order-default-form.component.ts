import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WorkOrderFormFrontEndDto } from '../../../../dtos/production/work-order-form-front-end-interface';
import { NgxPrintService, PrintOptions } from 'ngx-print';

@Component({
  selector: 'app-work-order-default-form',
  templateUrl: './work-order-default-form.component.html',
  styleUrls: ['./work-order-default-form.component.scss'],
})
export class WorkOrderDefaultFormComponent {
  constructor(private printService: NgxPrintService) {}
  @Input() workOrderForm: WorkOrderFormFrontEndDto;
  @Output() closeForm = new EventEmitter<any>();

  onClose() {
    this.closeForm.emit();
  }

  telephoneFormat(telephoneNumber: string) {}

  isPanelExists(workOrderForm: WorkOrderFormFrontEndDto): string {
    if (
      workOrderForm.workOrder.panelHeight ||
      (!workOrderForm.workOrder.panelWidth &&
        workOrderForm.workOrder.sidePanelHeight) ||
      workOrderForm.workOrder.sidePanelWidth
    )
      return 'Var';
    return 'Yok';
  }

  printForm() {
    const customPrintOptions: PrintOptions = new PrintOptions({
      printSectionId: 'print-section',
      printTitle: 'Üretim Formu',
      useExistingCss: true,
    });
    this.printService.print(customPrintOptions);
  }

  phoneFormat(telephoneNumber: string): string {
    var phoneFormatter = require('phone-formatter');
    return phoneFormatter.format(telephoneNumber, '(NNN) NNN-NNNN');
  }
}
