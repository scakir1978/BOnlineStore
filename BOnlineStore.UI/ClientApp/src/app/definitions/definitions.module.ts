import lottie from 'lottie-web';
import { ColorModule } from './color/color.module';
import { ColorGroupModule } from './color-group/color-group.module';
import { ModelGroupModule } from './model-group/model-group.module';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { defineElement } from 'lord-icon-element';
import { AssemblerModule } from './assembler/assembler.module';
import { CurrencyModule } from './currency/currency.module';

@NgModule({
  declarations: [],
  imports: [
    ModelGroupModule,
    ColorGroupModule,
    ColorModule,
    AssemblerModule,
    CurrencyModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DefinitionsModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}
