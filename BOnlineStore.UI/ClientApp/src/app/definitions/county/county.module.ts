import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { CountyComponent } from './county.component';
import { CountyService } from './county.service';

const routes: Routes = [
  {
    path: 'county',
    component: CountyComponent,
    canActivate: [AuthGuard],
    resolve: {
      County: CountyService,
    },
  },
];

@NgModule({
  declarations: [CountyComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [CountyService, TranslateService],
})
export class CountyModule {}
