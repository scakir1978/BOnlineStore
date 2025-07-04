import { AlertService } from './../../core/services/alert.service';
import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { WorkOrderComponent } from './work-order.component';
import { WorkOrderService } from './work-order.service';
import { WorkOrderDefaultFormComponent } from './work-order-forms/work-order-default-form/work-order-default-form.component';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  {
    path: 'work-order',
    component: WorkOrderComponent,
    canActivate: [AuthGuard],
    resolve: {
      WorkOrder: WorkOrderService,
    },
  },
];

@NgModule({
  declarations: [WorkOrderComponent, WorkOrderDefaultFormComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
    CommonModule,
  ],
  exports: [TranslateModule],
  providers: [WorkOrderService, TranslateService, AlertService],
})
export class WorkOrderModule {}
