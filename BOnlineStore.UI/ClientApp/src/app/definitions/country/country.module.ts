import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { CountryComponent } from './country.component';
import { CountryService } from './country.service';

const routes: Routes = [
  {
    path: 'country',
    component: CountryComponent,
    canActivate: [AuthGuard],
    resolve: {
        Country: CountryService,
    },
  },
];

@NgModule({
  declarations: [CountryComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [CountryService, TranslateService],
})
export class CountryModule {}
