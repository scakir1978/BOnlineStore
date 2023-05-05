import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { TemplateCodesComponent } from './template-codes.component';
import { TemplateCodesService } from './template-codes.service';

const routes: Routes = [
  {
    path: 'template-codes',
    component: TemplateCodesComponent,
    canActivate: [AuthGuard],
    resolve: {
        TemplateCodes: TemplateCodesService,
    },
  },
];

@NgModule({
  declarations: [TemplateCodesComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [TemplateCodesService, TranslateService],
})
export class TemplateCodesModule {}
