import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { MeasurementAssemblyLimitsComponent } from './measurement-assembly-limits.component';
import { MeasurementAssemblyLimitsService } from './measurement-assembly-limits.service';

const routes: Routes = [
  {
    path: 'measurement-assembly-limits',
    component: MeasurementAssemblyLimitsComponent,
    canActivate: [AuthGuard],
    resolve: {
        MeasurementAssemblyLimits: MeasurementAssemblyLimitsService,
    },
  },
];

@NgModule({
  declarations: [MeasurementAssemblyLimitsComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [MeasurementAssemblyLimitsService, TranslateService],
})
export class MeasurementAssemblyLimitsModule {}
