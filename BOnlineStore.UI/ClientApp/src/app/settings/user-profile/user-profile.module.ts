import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DevExtremeModule } from 'devextreme-angular';
import { TranslateModule } from '@ngx-translate/core';
import { UserProfileComponent } from './user-profile.component';
import { AuthGuard } from '../../core/guards/auth.guard';
import { SharedModule } from '../../shared/shared.module';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';

const routes: Routes = [
  // /settings/user-profile
  {
    path: 'user-profile',
    component: UserProfileComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  declarations: [UserProfileComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    DevExtremeModule,
    TranslateModule,
    SharedModule,
    LeafletModule,
  ],
  exports: [RouterModule],
})
export class UserProfileModule {}
