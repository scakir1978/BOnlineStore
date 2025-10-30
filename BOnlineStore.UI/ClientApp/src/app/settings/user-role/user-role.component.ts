import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { UserRoleService } from './user-role.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.scss'],
})
export class UserRoleComponent extends BaseDefinitionsOnGridComponent {
  public userRoleDataSource: DataSource;
  public roleDataSource: CustomStore;
  public userDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _userRoleService: UserRoleService
  ) {
    super(
      _translate,
      'USERROLE', //Pdf, excel dosya adı
      'USERROLE', //breadCrump için kullanılacak componenet keyi
      'SETTINGS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.userRoleDataSource = _userRoleService.getDataSource();
    this.roleDataSource = _userRoleService.getRoleDataSource();
    this.userDataSource = _userRoleService.getUserDataSource();
  }
}
