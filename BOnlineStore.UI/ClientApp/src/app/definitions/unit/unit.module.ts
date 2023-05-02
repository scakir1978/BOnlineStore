import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { UnitComponent } from './unit.component';
import { UnitService } from './unit.service';

const routes: Routes = [
  {
    path: 'unit',
    component: UnitComponent,
    canActivate: [AuthGuard],
    resolve: {
        Unit: UnitService,
    },
  },
];

@NgModule({
  declarations: [UnitComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [UnitService, TranslateService],
})
export class UnitModule {}
