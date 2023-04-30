import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { TemplateComponent } from './template.component';
import { TemplateService } from './template.service';

const routes: Routes = [
  {
    path: 'template',
    component: TemplateComponent,
    canActivate: [AuthGuard],
    resolve: {
        Template: TemplateService,
    },
  },
];

@NgModule({
  declarations: [TemplateComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [TemplateService, TranslateService],
})
export class TemplateModule {}
