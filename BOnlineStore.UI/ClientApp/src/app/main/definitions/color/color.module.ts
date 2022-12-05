import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreCommonModule } from '@core/common.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from 'app/auth/helpers';

import { ContentHeaderModule } from 'app/layout/components/content-header/content-header.module';
import { Ng2FlatpickrModule } from 'ng2-flatpickr';
import { ColorComponent } from './color.component';
import { ColorService } from './color.service';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

const routes: Routes = [
  {
    path: 'color',
    component: ColorComponent,
    canActivate: [AuthGuard],
    resolve: {
      ColorGroup: ColorService,
    },
    data: { animation: 'color' },
  },
];

@NgModule({
  declarations: [ColorComponent],
  imports: [
    RouterModule.forChild(routes),
    NgbModule,
    ContentHeaderModule,
    CoreCommonModule,
    ContentHeaderModule,
    Ng2FlatpickrModule,
    TranslateModule,
    DevExtremeModule,
    SweetAlert2Module.forRoot(),
  ],
  exports: [TranslateModule],
  providers: [ColorService, TranslateService],
})
export class ColorModule {}
