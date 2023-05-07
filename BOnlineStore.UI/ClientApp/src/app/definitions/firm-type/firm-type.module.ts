import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { FirmTypeComponent } from './firm-type.component';
import { FirmTypeService } from './firm-type.service';

const routes: Routes = [
  {
    path: 'firm-type',
    component: FirmTypeComponent,
    canActivate: [AuthGuard],
    resolve: {
        FirmType: FirmTypeService,
    },
  },
];

@NgModule({
  declarations: [FirmTypeComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [FirmTypeService, TranslateService],
})
export class FirmTypeModule {}
