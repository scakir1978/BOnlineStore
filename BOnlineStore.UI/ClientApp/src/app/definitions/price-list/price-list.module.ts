import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { PriceListComponent } from './price-list.component';
import { PriceListService } from './price-list.service';

const routes: Routes = [
  {
    path: 'price-list',
    component: PriceListComponent,
    canActivate: [AuthGuard],
    resolve: {
        PriceList: PriceListService,
    },
  },
];

@NgModule({
  declarations: [PriceListComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [PriceListService, TranslateService],
})
export class PriceListModule {}
