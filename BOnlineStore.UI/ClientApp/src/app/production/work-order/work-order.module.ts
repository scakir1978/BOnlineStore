import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { WorkOrderComponent } from './work-order.component';
import { WorkOrderService } from './work-order.service';

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
  declarations: [WorkOrderComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [WorkOrderService, TranslateService],
})
export class WorkOrderModule {}
