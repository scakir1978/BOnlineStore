import { DeliveryAdressModule } from './delivery-adress/delivery-adress.module';
import { PriceListModule } from './price-list/price-list.module';
import { FirmModule } from './firm/firm.module';
import { DistrictModule } from './district/district.module';
import { CountryModule } from './country/country.module';
import { CountyModule } from './county/county.module';
import { CityModule } from './city/city.module';
import { ExchangeRateModule } from './exchange-rate/exchange-rate.module';
import { AssemblyPriceModule } from './assembly-price/assembly-price.module';
import { MeasurementAssemblyLimitsModule } from './measurement-assembly-limits/measurement-assembly-limits.module';
import { ModelModule } from './model/model.module';
import { PanelModule } from './panel/panel.module';
import { RecipeTypeModule } from './recipe-type/recipe-type.module';
import { RawMaterialModule } from './raw-material/raw-material.module';
import { FirmTypeModule } from './firm-type/firm-type.module';
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
import { GlassModule } from './glass/glass.module';
import { GlassGroupModule } from './glass-group/glass-group.module';

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
    GlassModule,
    GlassGroupModule,
    FirmTypeModule,
    RawMaterialModule,
    RecipeTypeModule,
    PanelModule,
    ModelModule,
    MeasurementAssemblyLimitsModule,
    AssemblyPriceModule,
    ExchangeRateModule,
    CityModule,
    CountyModule,
    CountryModule,
    DistrictModule,
    FirmModule,
    PriceListModule,
    DeliveryAdressModule,
],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DefinitionsModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}
