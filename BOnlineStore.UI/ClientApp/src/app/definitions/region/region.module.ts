import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RegionComponent } from './region.component';
import { RegionService } from './region.service';

const routes: Routes = [
  {
    path: 'region',
    component: RegionComponent,
    canActivate: [AuthGuard],
    resolve: {
        Region: RegionService,
    },
  },
];

@NgModule({
  declarations: [RegionComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [RegionService, TranslateService],
})
export class RegionModule {}
