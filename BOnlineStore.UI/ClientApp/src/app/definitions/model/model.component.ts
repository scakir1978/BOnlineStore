import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { ModelService } from './model.service';
import { Component, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NoPictureBase64Class } from '../../base-classes/shared/no-picture-base64-class';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { ValueChangedEvent } from 'devextreme/ui/file_uploader';

@Component({
  selector: 'model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.scss'],
})
export class ModelComponent extends BaseDefinitionsOnGridComponent {
  public modelDataSource: DataSource;
  public panelDataSource: CustomStore;
  public recipeTypeDataSource: CustomStore;
  public modelGroupDataSource: CustomStore;
  public fileSize: number = 0;

  public noPicture: string = '';
  public pictureStyle: string = 'object-fit: contain; width: 10%; height: 10%';
  public noPictureStyle: string = 'object-fit: contain; width: 5%; height: 10%';

  @ViewChild('uploadedImage') uploadedImageRef!: HTMLImageElement;

  constructor(
    public override _translate: TranslateService,
    private _modelService: ModelService
  ) {
    super(
      _translate,
      'MODEL', //Pdf, excel dosya adı
      'MODEL', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );

    this.validateFileSize = this.validateFileSize.bind(this);

    this.modelDataSource = _modelService.getDataSource();
    this.panelDataSource = _modelService.getPanelDataSource();
    this.recipeTypeDataSource = _modelService.getRecipeTypeDataSource();
    this.modelGroupDataSource = _modelService.getModelGroupDataSource();

    this.noPicture = NoPictureBase64Class.getBase64128();
  }

  onValueChanged(e: any, cellInfo: any): void {
    const reader: FileReader = new FileReader();
    this.fileSize = e.value![0].size;
    reader.onload = (args) => {
      if (typeof args.target?.result === 'string') {
        this.uploadedImageRef.src = args.target.result;
        this.isPictureNotLoaded(args.target.result);
        cellInfo.setValue(args.target.result);
      }
    };
    reader.readAsDataURL(e.value![0]); // convert to base64 string
  }

  isPictureNotLoaded(pictureData): boolean {
    if (pictureData) return false;
    return true;
  }

  validateFileSize(e): boolean {
    if (this.fileSize > 307200) return false; //Dosya boyutu 300Kb den büyük olamaz.
    return true;
  }

  onEditingStart(e) {
    this.fileSize = 0;
  }
}
