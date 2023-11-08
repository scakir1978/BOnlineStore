import { WorkOrderModule } from './work-order/work-order.module';
import { FormulaModule } from './formula/formula.module';
import { FormulaTypeModule } from './formula-type/formula-type.module';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import lottie from 'lottie-web';
import { defineElement } from 'lord-icon-element';

@NgModule({
  imports: [FormulaTypeModule, FormulaModule, WorkOrderModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProductionModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}
