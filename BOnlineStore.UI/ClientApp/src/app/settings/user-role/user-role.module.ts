import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { UserRoleComponent } from './user-role.component';
import { UserRoleService } from './user-role.service';

const routes: Routes = [
  {
    path: 'user-role',
    component: UserRoleComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  declarations: [UserRoleComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [UserRoleService, TranslateService],
})
export class UserRoleModule {}
