import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { LengthComponent } from './length.component';
import { LengthService } from './length.service';

const routes: Routes = [
  {
    path: 'length',
    component: LengthComponent,
    canActivate: [AuthGuard],
    resolve: {
        Length: LengthService,
    },
  },
];

@NgModule({
  declarations: [LengthComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [LengthService, TranslateService],
})
export class LengthModule {}
