import { TemplateCodesModule } from './template-codes/template-codes.module';
import { UnitModule } from './unit/unit.module';
import { LengthModule } from './length/length.module';
import { BankModule } from './bank/bank.module';
import { ColorModule } from './color/color.module';
import { ColorGroupModule } from './color-group/color-group.module';
import { ModelGroupModule } from './model-group/model-group.module';
import { AssemblerModule } from './assembler/assembler.module';
import { CurrencyModule } from './currency/currency.module';
import { ExpenseModule } from './expense/expense.module';
import { RawMaterialGroupModule } from './raw-material-group/raw-material-group.module';
import { RegionModule } from './region/region.module';
import lottie from 'lottie-web';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { defineElement } from 'lord-icon-element';

@NgModule({
  imports: [
    ModelGroupModule,
    ColorGroupModule,
    ColorModule,
    AssemblerModule,
    CurrencyModule,
    ExpenseModule,
    RawMaterialGroupModule,
    RegionModule,
    BankModule,
    LengthModule,
    UnitModule,
    TemplateCodesModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DefinitionsModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}
