import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { AssemblyPriceComponent } from './assembly-price.component';
import { AssemblyPriceService } from './assembly-price.service';

const routes: Routes = [
  {
    path: 'assembly-price',
    component: AssemblyPriceComponent,
    canActivate: [AuthGuard],
    resolve: {
        AssemblyPrice: AssemblyPriceService,
    },
  },
];

@NgModule({
  declarations: [AssemblyPriceComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [AssemblyPriceService, TranslateService],
})
export class AssemblyPriceModule {}
