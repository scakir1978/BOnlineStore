import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { ModelComponent } from './model.component';
import { ModelService } from './model.service';

const routes: Routes = [
  {
    path: 'model',
    component: ModelComponent,
    canActivate: [AuthGuard],
    resolve: {
      Model: ModelService,
    },
  },
];

@NgModule({
  declarations: [ModelComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ModelService, TranslateService],
})
export class ModelModule {}
