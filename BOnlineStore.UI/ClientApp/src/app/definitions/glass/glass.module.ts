import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { GlassComponent } from './glass.component';
import { GlassService } from './glass.service';

const routes: Routes = [
  {
    path: 'glass',
    component: GlassComponent,
    canActivate: [AuthGuard],
    resolve: {
      GlassGroup: GlassService,
    },
  },
];

@NgModule({
  declarations: [GlassComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [GlassService, TranslateService],
})
export class GlassModule {}
