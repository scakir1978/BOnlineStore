import { defineElement } from 'lord-icon-element';
import { CommonModule } from '@angular/common';
import { SharedModule } from './../../shared/shared.module';
import { AuthGuard } from './../../core/guards/auth.guard';
import { DevExtremeModule } from 'devextreme-angular';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ModelGroupService } from './model-group.service';
import { ModelGroupComponent } from './model-group.component';
import lottie from 'lottie-web';

const routes: Routes = [
  {
    path: 'model-groups',
    component: ModelGroupComponent,
    canActivate: [AuthGuard],
    resolve: {
      modelGroup: ModelGroupService,
    },
  },
];

@NgModule({
  declarations: [ModelGroupComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [ModelGroupService, TranslateService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ModelGroupModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}
