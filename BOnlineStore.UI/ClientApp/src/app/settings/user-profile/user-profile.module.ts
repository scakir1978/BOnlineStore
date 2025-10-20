import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DevExtremeModule } from 'devextreme-angular';
import { TranslateModule } from '@ngx-translate/core';
import { UserProfileComponent } from './user-profile.component';
import { AuthGuard } from '../../core/guards/auth.guard';

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
  ],
  exports: [RouterModule],
})
export class UserProfileModule {}
