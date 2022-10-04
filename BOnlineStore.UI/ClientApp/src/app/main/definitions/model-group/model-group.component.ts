import { ModelGroupService } from './model-group.service';
import { Component, OnInit } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { CoreConfig } from '@core/types';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'model-group',
  templateUrl: './model-group.component.html',
  styleUrls: ['./model-group.component.scss']
})
export class ModelGroupComponent implements OnInit {
  // public
  public contentHeader: object;

  public modelGroupDataSource: DataSource;

  private _unsubscribeAll: Subject<any>;

  constructor(private _translate: TranslateService,
    private _coreConfigService: CoreConfigService, private _modelGroupService : ModelGroupService) {
      this.modelGroupDataSource = _modelGroupService.getDataSource();
      this._unsubscribeAll = new Subject();
    }
    

  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit() {    

    this.createBreadCrumb();

    this._coreConfigService.getConfig().pipe(takeUntil(this._unsubscribeAll)).subscribe((response : CoreConfig)=>{ 
      this.createBreadCrumb();
    })  
  }

  private createBreadCrumb(){
    this.contentHeader = {
      headerTitle: this._translate.instant("KEYS.MODELGROUPS"),
      actionButton: true,
      breadcrumb: {
        type: '',
        links: [
          {
            name: this._translate.instant("KEYS.DEFINITIONS"),
            isLink: false            
          },
          {
            name: this._translate.instant("KEYS.MODELGROUPS"),
            isLink: false,
          },
        ],
      },
    };
  }

  onSaving(e){
    
  }
}
