import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { ExchangeRateComponent } from './exchange-rate.component';
import { ExchangeRateService } from './exchange-rate.service';

const routes: Routes = [
  {
    path: 'exchange-rate',
    component: ExchangeRateComponent,
    canActivate: [AuthGuard],
    resolve: {
      ExchangeRate: ExchangeRateService,
    },
  },
];

@NgModule({
  declarations: [ExchangeRateComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ExchangeRateService, TranslateService],
})
export class ExchangeRateModule {}
