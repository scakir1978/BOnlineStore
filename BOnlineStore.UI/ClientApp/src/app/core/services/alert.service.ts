import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  constructor(public _translate: TranslateService) {}

  showErrorMessage(errorMessage) {
    Swal.fire({
      title: this._translate.instant('SAVERECORDERROR'),
      text: errorMessage,
      icon: 'error',
      confirmButtonColor: '#364574',
      confirmButtonText: this._translate.instant('OK'),
    });
  }

  showSuccessMessage(successMessage) {
    Swal.fire({
      title: this._translate.instant('INFORMATION'),
      text: successMessage,
      icon: 'success',
      confirmButtonColor: '#364574',
      confirmButtonText: this._translate.instant('OK'),
    });
  }
}
