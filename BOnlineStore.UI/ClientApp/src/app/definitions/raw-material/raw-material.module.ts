import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RawMaterialComponent } from './raw-material.component';
import { RawMaterialService } from './raw-material.service';

const routes: Routes = [
  {
    path: 'raw-material',
    component: RawMaterialComponent,
    canActivate: [AuthGuard],
    resolve: {
        RawMaterial: RawMaterialService,
    },
  },
];

@NgModule({
  declarations: [RawMaterialComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [RawMaterialService, TranslateService],
})
export class RawMaterialModule {}
