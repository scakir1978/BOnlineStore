import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { UserService } from './user.service';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';
import {
  TimezoneService,
  TimeZoneOption,
} from 'app/core/services/timezone.service';

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent
  extends BaseDefinitionsOnGridComponent
  implements OnInit
{
  public userDataSource: DataSource;

  // Gender options (labels localized)
  genderOptions: Array<{ value: string; text: string }> = [];

  // Locale options
  localeOptions: Array<{ value: string; text: string }> = [];

  // Timezone options
  timeZoneOptions: TimeZoneOption[] = [];

  maxBirthDate = new Date();

  // Track if we're in edit mode (true) or add mode (false)
  isEditMode = false;

  constructor(
    public override _translate: TranslateService,
    private _userService: UserService,
    private timezoneService: TimezoneService
  ) {
    super(
      _translate,
      'USER', //Pdf, excel dosya adı
      'USER', //breadCrump için kullanılacak componenet keyi
      'SETTINGS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.userDataSource = _userService.getDataSource();
  }

  private t(key: string): string {
    return this._translate.instant(key);
  }

  private updateLocalizedOptions(): void {
    this.genderOptions = [
      { value: 'Male', text: this.t('USER_PROFILE.GENDER_MALE') },
      { value: 'Female', text: this.t('USER_PROFILE.GENDER_FEMALE') },
      { value: 'Other', text: this.t('USER_PROFILE.GENDER_OTHER') },
    ];

    this.localeOptions = [
      { value: 'tr-TR', text: this.t('USER_PROFILE.LOCALE_TR') },
      { value: 'en-US', text: this.t('USER_PROFILE.LOCALE_EN') },
      { value: 'de-DE', text: this.t('USER_PROFILE.LOCALE_DE') },
    ];
  }

  ngOnInit(): void {
    // initialize localized option labels and react to language changes
    this.updateLocalizedOptions();
    this._translate.onLangChange.subscribe(() => this.updateLocalizedOptions());

    // Load timezone options in background
    this.timezoneService.getTimeZones().subscribe((zones) => {
      this.timeZoneOptions = zones;
    });
  }

  // Event handler when starting to add a new row
  onInitNewRow = (e: any) => {
    this.isEditMode = false;
  };

  // Event handler when starting to edit an existing row
  onEditingStart = (e: any) => {
    this.isEditMode = true;
  };

  // Validate password with medium complexity
  // Min 8 characters, at least 1 uppercase, 1 lowercase
  validatePassword = (options: any) => {
    const password = options.value;

    if (!password && this.isEditMode) {
      // Password not required in edit mode
      return true;
    }

    if (!password && !this.isEditMode) {
      // Password required in add mode
      return false;
    }

    // Medium complexity: Min 8 characters, at least 1 uppercase, 1 lowercase
    return (
      password.length >= 8 && /[a-z]/.test(password) && /[A-Z]/.test(password)
    );
  };

  // Validate password confirmation
  validatePasswordConfirmation = (options: any) => {
    if (!options.value && this.isEditMode) {
      // Password confirmation not required in edit mode
      return true;
    }

    if (!options.value && !this.isEditMode) {
      // Password confirmation required in add mode
      return false;
    }

    return options.value === options.data.password;
  };
}
