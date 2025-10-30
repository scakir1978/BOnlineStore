import { UserRoleModule } from './user-role/user-role.module';
import { RoleModule } from './role/role.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileModule } from './user-profile/user-profile.module';

@NgModule({
  declarations: [],
  imports: [CommonModule, UserProfileModule, RoleModule, UserRoleModule],
})
export class SettingsModule {}
