import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { ExpenseComponent } from './expense.component';
import { ExpenseService } from './expense.service';

const routes: Routes = [
  {
    path: 'expense',
    component: ExpenseComponent,
    canActivate: [AuthGuard],
    resolve: {
      Expense: ExpenseService,
    },
  },
];

@NgModule({
  declarations: [ExpenseComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ExpenseService, TranslateService],
})
export class ExpenseModule {}
