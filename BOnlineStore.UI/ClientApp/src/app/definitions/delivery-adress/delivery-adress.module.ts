import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { DeliveryAdressComponent } from './delivery-adress.component';
import { DeliveryAdressService } from './delivery-adress.service';

const routes: Routes = [
  {
    path: 'delivery-adress',
    component: DeliveryAdressComponent,
    canActivate: [AuthGuard],
    resolve: {
        DeliveryAdress: DeliveryAdressService,
    },
  },
];

@NgModule({
  declarations: [DeliveryAdressComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [DeliveryAdressService, TranslateService],
})
export class DeliveryAdressModule {}
