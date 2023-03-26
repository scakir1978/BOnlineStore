import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { ColorComponent } from './color.component';
import { ColorService } from './color.service';

const routes: Routes = [
  {
    path: 'color',
    component: ColorComponent,
    canActivate: [AuthGuard],
    resolve: {
      ColorGroup: ColorService,
    },
  },
];

@NgModule({
  declarations: [ColorComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ColorService, TranslateService],
})
export class ColorModule {}
