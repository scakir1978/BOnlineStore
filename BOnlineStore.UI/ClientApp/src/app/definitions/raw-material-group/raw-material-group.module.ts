import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RawMaterialGroupComponent } from './raw-material-group.component';
import { RawMaterialGroupService } from './raw-material-group.service';

const routes: Routes = [
  {
    path: 'raw-material-groups',
    component: RawMaterialGroupComponent,
    canActivate: [AuthGuard],
    resolve: {
      RawMaterialGroup: RawMaterialGroupService,
    },
  },
];

@NgModule({
  declarations: [RawMaterialGroupComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [RawMaterialGroupService, TranslateService],
})
export class RawMaterialGroupModule {}
