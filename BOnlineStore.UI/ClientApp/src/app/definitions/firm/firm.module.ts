import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { FirmComponent } from './firm.component';
import { FirmService } from './firm.service';

const routes: Routes = [
  {
    path: 'firm',
    component: FirmComponent,
    canActivate: [AuthGuard],
    resolve: {
        Firm: FirmService,
    },
  },
];

@NgModule({
  declarations: [FirmComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [FirmService, TranslateService],
})
export class FirmModule {}
