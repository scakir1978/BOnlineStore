import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { PanelService } from './panel.service';
import { Component, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { DxFileUploaderComponent } from 'devextreme-angular';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { UploadedEvent, ValueChangedEvent } from 'devextreme/ui/file_uploader';

@Component({
  selector: 'panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.scss'],
})
export class PanelComponent extends BaseDefinitionsOnGridComponent {
  public panelDataSource: DataSource;
  public recipeTypeDataSource: CustomStore;
  public modelGroupDataSource: CustomStore;
  public isEditRecord: boolean = false;

  @ViewChild('uploadedImage') uploadedImageRef!: HTMLImageElement;

  constructor(
    public override _translate: TranslateService,
    private _panelService: PanelService,
    private sanitizer: DomSanitizer
  ) {
    super(
      _translate,
      'PANEL', //Pdf, excel dosya adı
      'PANEL', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.panelDataSource = _panelService.getDataSource();
    this.modelGroupDataSource = _panelService.getModelGroupDataSource();
    this.recipeTypeDataSource = _panelService.getRecipeTypeDataSource();
  }

  getBase64Image(base64Image: any) {
    if (!base64Image) return null;
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      'data:image/png;base64, ' + base64Image
    );
  }

  onValueChanged(e: ValueChangedEvent, cellInfo: any): void {
    this.isEditRecord = true;
    const reader: FileReader = new FileReader();
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

  onInitNewRow(e: any) {
    this.isEditRecord = false;
  }
}
