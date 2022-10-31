import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreCommonModule } from '@core/common.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from 'app/auth/helpers';

import { ContentHeaderModule } from 'app/layout/components/content-header/content-header.module';
import { Ng2FlatpickrModule } from 'ng2-flatpickr';
import { ModelGroupComponent } from './model-group.component';
import { ModelGroupService } from './model-group.service';

const routes: Routes = [
  {
    path: 'model-groups',
    component: ModelGroupComponent,
    canActivate: [AuthGuard],
    resolve: {
      modelGroup: ModelGroupService,
    },
    data: { animation: 'model-groups' },
  },
];

@NgModule({
  declarations: [ModelGroupComponent],
  imports: [
    RouterModule.forChild(routes),
    NgbModule,
    ContentHeaderModule,
    CoreCommonModule,
    ContentHeaderModule,
    Ng2FlatpickrModule,
    TranslateModule,
    DevExtremeModule,
  ],
  exports: [TranslateModule],
  providers: [ModelGroupService, TranslateService],
})
export class ModelGroupModule {}
