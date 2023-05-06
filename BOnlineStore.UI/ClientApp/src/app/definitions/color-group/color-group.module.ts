import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { ColorGroupComponent } from './color-group.component';
import { ColorGroupService } from './color-group.service';

const routes: Routes = [
  {
    path: 'color-group',
    component: ColorGroupComponent,
    canActivate: [AuthGuard],
    resolve: {
      ColorGroup: ColorGroupService,
    },
  },
];

@NgModule({
  declarations: [ColorGroupComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ColorGroupService, TranslateService],
})
export class ColorGroupModule {}
