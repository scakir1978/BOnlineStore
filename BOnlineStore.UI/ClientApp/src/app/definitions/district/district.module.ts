import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { DistrictComponent } from './district.component';
import { DistrictService } from './district.service';

const routes: Routes = [
  {
    path: 'district',
    component: DistrictComponent,
    canActivate: [AuthGuard],
    resolve: {
        District: DistrictService,
    },
  },
];

@NgModule({
  declarations: [DistrictComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [DistrictService, TranslateService],
})
export class DistrictModule {}
