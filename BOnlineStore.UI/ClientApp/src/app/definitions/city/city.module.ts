import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { CityComponent } from './city.component';
import { CityService } from './city.service';

const routes: Routes = [
  {
    path: 'city',
    component: CityComponent,
    canActivate: [AuthGuard],
    resolve: {
        City: CityService,
    },
  },
];

@NgModule({
  declarations: [CityComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [CityService, TranslateService],
})
export class CityModule {}
