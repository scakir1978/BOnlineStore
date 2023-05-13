import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { PanelComponent } from './panel.component';
import { PanelService } from './panel.service';

const routes: Routes = [
  {
    path: 'panel',
    component: PanelComponent,
    canActivate: [AuthGuard],
    resolve: {
      Panel: PanelService,
    },
  },
];

@NgModule({
  declarations: [PanelComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [PanelService, TranslateService],
})
export class PanelModule {}
