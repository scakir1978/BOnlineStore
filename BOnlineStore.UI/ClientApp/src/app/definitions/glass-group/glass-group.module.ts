import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { GlassGroupComponent } from './glass-group.component';
import { GlassGroupService } from './glass-group.service';

const routes: Routes = [
  {
    path: 'glass-group',
    component: GlassGroupComponent,
    canActivate: [AuthGuard],
    resolve: {
      GlassGroup: GlassGroupService,
    },
  },
];

@NgModule({
  declarations: [GlassGroupComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [GlassGroupService, TranslateService],
})
export class GlassGroupModule {}
