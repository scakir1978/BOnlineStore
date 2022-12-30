import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreCommonModule } from '@core/common.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from 'app/auth/helpers';

import { ContentHeaderModule } from 'app/layout/components/content-header/content-header.module';
import { Ng2FlatpickrModule } from 'ng2-flatpickr';
import { FormulaTypeComponent } from './formula-type.component';
import { FormulaTypeService } from './formula-type.service';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

const routes: Routes = [
  {
    path: 'formula-type',
    component: FormulaTypeComponent,
    canActivate: [AuthGuard],
    resolve: {
      FormulaType: FormulaTypeService,
    },
    data: { animation: 'formula-type' },
  },
];

@NgModule({
  declarations: [FormulaTypeComponent],
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
  providers: [FormulaTypeService, TranslateService],
})
export class FormulaTypeModule {}
