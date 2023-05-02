import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { BankComponent } from './bank.component';
import { BankService } from './bank.service';

const routes: Routes = [
  {
    path: 'bank',
    component: BankComponent,
    canActivate: [AuthGuard],
    resolve: {
        Bank: BankService,
    },
  },
];

@NgModule({
  declarations: [BankComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [BankService, TranslateService],
})
export class BankModule {}
