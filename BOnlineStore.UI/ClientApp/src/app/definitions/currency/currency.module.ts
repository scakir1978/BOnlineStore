import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { CurrencyComponent } from './currency.component';
import { CurrencyService } from './currency.service';

const routes: Routes = [
  {
    path: 'currency',
    component: CurrencyComponent,
    canActivate: [AuthGuard],
    resolve: {
      Currency: CurrencyService,
    },
  },
];

@NgModule({
  declarations: [CurrencyComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [CurrencyService, TranslateService],
})
export class CurrencyModule {}
