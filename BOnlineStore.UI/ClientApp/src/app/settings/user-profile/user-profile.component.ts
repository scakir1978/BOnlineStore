import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import notify from 'devextreme/ui/notify';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationService } from 'app/core/services/auth.service';
import { UserProfileService } from 'app/settings/user-profile/user-profile.service';
import {
  TimezoneService,
  TimeZoneOption,
} from 'app/core/services/timezone.service';
import {
  UserProfileDto,
  UserProfileUpdateDto,
} from 'app/dtos/settings/user-profile.dto';
import { ChangePasswordDto } from 'app/dtos/settings/change-password.dto';
import { PasswordValidatorService } from 'app/base-classes/shared/password-validator.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
  @Input() userEmail: string = '';

  userProfile: UserProfileDto | null = null;
  isLoading = false;
  isSaving = false;

  // Change Password popup
  isChangePasswordPopupVisible = false;
  changePasswordData: ChangePasswordDto = {
    userId: '',
    currentPassword: '',
    newPassword: '',
    confirmPassword: '',
  };
  isChangingPassword = false;

  // Gender options (labels localized)
  genderOptions: Array<{ value: string; text: string }> = [];

  // Locale options
  localeOptions: Array<{ value: string; text: string }> = [];

  // Timezone options
  timeZoneOptions: TimeZoneOption[] = [];

  maxBirthDate = new Date();

  // bread crumb items
  breadCrumbItems!: Array<{}>;

  // Password visibility button options
  currentPasswordButtonOptions: any;
  newPasswordButtonOptions: any;
  confirmPasswordButtonOptions: any;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private userProfileService: UserProfileService,
    private translate: TranslateService,
    private timezoneService: TimezoneService,
    private passwordValidator: PasswordValidatorService
  ) {}

  private t(key: string): string {
    return this.translate.instant(key);
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

    this.breadCrumbItems = [
      { label: this.t('SETTINGS') },
      { label: this.t('USER_PROFILE.TITLE'), active: true },
    ];
  }

  ngOnInit(): void {
    // initialize localized option labels and react to language changes
    this.updateLocalizedOptions();
    this.translate.onLangChange.subscribe(() => this.updateLocalizedOptions());

    // Initialize password visibility button options
    this.initializePasswordButtons();

    // Öncelik: query param -> route param -> @Input -> authenticated user
    const queryEmail = this.route.snapshot.queryParamMap.get('email');
    if (queryEmail) {
      this.userEmail = queryEmail;
    } else {
      const routeEmail = this.route.snapshot.paramMap.get('email');
      if (routeEmail) {
        this.userEmail = routeEmail;
      }
    }

    if (!this.userEmail) {
      const currentUser = this.authService.currentUser();
      if (currentUser && currentUser.email) {
        this.userEmail = currentUser.email;
      }
    }

    if (this.userEmail) {
      this.loadUserProfile();
    } else {
      notify(this.t('USER_PROFILE.USER_NOT_FOUND'), 'error', 3000);
    }

    // Load timezone options in background
    this.timezoneService.getTimeZones().subscribe((zones) => {
      this.timeZoneOptions = zones;
    });
  }

  loadUserProfile(): void {
    this.isLoading = true;
    this.userProfileService.getUserByEmail(this.userEmail).subscribe({
      next: (data) => {
        this.userProfile = data;
        if (this.userProfile?.birthdate) {
          this.userProfile.birthdate = new Date(this.userProfile.birthdate);
        }
        if (this.userProfile && !this.userProfile.zoneInfo) {
          try {
            const sysTz = Intl.DateTimeFormat().resolvedOptions().timeZone;
            if (sysTz) {
              this.userProfile.zoneInfo = sysTz;
            }
          } catch {}
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Kullanıcı profili yüklenirken hata:', err);
        notify(this.t('USER_PROFILE.LOAD_ERROR'), 'error', 3000);
        this.isLoading = false;
      },
    });
  }

  saveUserProfile(e: SubmitEvent): void {
    if (!this.userProfile) return;
    this.isSaving = true;

    const dto: UserProfileUpdateDto = {
      id: this.userProfile.id,
      email: this.userProfile.email,
      phoneNumber: this.userProfile.phoneNumber,
      locale: this.userProfile.locale,
      name: this.userProfile.name,
      familyName: this.userProfile.familyName,
      givenName: this.userProfile.givenName,
      middleName: this.userProfile.middleName,
      nickname: this.userProfile.nickname,
      preferredUsername: this.userProfile.preferredUsername,
      profile: this.userProfile.profile,
      picture: this.userProfile.picture,
      website: this.userProfile.website,
      gender: this.userProfile.gender,
      birthdate: this.userProfile.birthdate,
      zoneInfo: this.userProfile.zoneInfo,
    };

    this.userProfileService.updateUserProfile(dto).subscribe({
      next: (data) => {
        this.userProfile = data;
        if (this.userProfile?.birthdate) {
          this.userProfile.birthdate = new Date(this.userProfile.birthdate);
        }
        notify(this.t('USER_PROFILE.UPDATE_SUCCESS'), 'success', 3000);
        this.isSaving = false;
      },
      error: (err) => {
        console.error('Kullanıcı profili güncellenirken hata:', err);
        notify(this.t('USER_PROFILE.UPDATE_ERROR'), 'error', 3000);
        this.isSaving = false;
      },
    });

    e.preventDefault();
  }

  // Initialize password visibility button options
  initializePasswordButtons(): void {
    this.currentPasswordButtonOptions = {
      icon: 'eyeclose',
      type: 'default',
      stylingMode: 'text',
      onClick: (e: any) => {
        this.togglePasswordVisibility(e, 'currentPassword');
      },
    };

    this.newPasswordButtonOptions = {
      icon: 'eyeclose',
      type: 'default',
      stylingMode: 'text',
      onClick: (e: any) => {
        this.togglePasswordVisibility(e, 'newPassword');
      },
    };

    this.confirmPasswordButtonOptions = {
      icon: 'eyeclose',
      type: 'default',
      stylingMode: 'text',
      onClick: (e: any) => {
        this.togglePasswordVisibility(e, 'confirmPassword');
      },
    };
  }

  // Toggle password visibility
  togglePasswordVisibility(buttonEvent: any, fieldName: string): void {
    const textBox = buttonEvent.component.element().closest('.dx-textbox');
    const input = textBox?.querySelector('input');

    if (input) {
      if (input.type === 'password') {
        input.type = 'text';
        buttonEvent.component.option('icon', 'eyeopen');
      } else {
        input.type = 'password';
        buttonEvent.component.option('icon', 'eyeclose');
      }
    }
  }

  // Open change password popup
  openChangePasswordPopup(): void {
    if (!this.userProfile?.id) {
      notify(this.t('USER_PROFILE.USER_NOT_FOUND'), 'error', 3000);
      return;
    }

    this.changePasswordData = {
      userId: this.userProfile.id,
      currentPassword: '',
      newPassword: '',
      confirmPassword: '',
    };

    // Reinitialize password buttons to reset their state
    this.initializePasswordButtons();

    this.isChangePasswordPopupVisible = true;
  }

  // Close change password popup
  closeChangePasswordPopup(): void {
    this.isChangePasswordPopupVisible = false;
    this.changePasswordData = {
      userId: '',
      currentPassword: '',
      newPassword: '',
      confirmPassword: '',
    };
  }

  // Validate password with medium complexity
  // Min 8 characters, at least 1 uppercase, 1 lowercase, 1 digit, 1 special character
  validatePassword = (options: any) => {
    const password = options.value;

    if (!password) {
      return false;
    }

    return this.passwordValidator.validatePasswordStrength(password);
  };

  // Validate password confirmation
  validatePasswordConfirmation = (options: any) => {
    if (!options.value) {
      return false;
    }

    return options.value === options.data.newPassword;
  };

  // Submit change password
  submitChangePassword(e: SubmitEvent): void {
    if (!this.changePasswordData.userId) {
      notify(this.t('USER_PROFILE.CHANGE_PASSWORD.ERROR'), 'error', 3000);
      return;
    }

    this.isChangingPassword = true;

    this.userProfileService.changePassword(this.changePasswordData).subscribe({
      next: () => {
        notify(this.t('USER_PROFILE.CHANGE_PASSWORD.SUCCESS'), 'success', 3000);
        this.closeChangePasswordPopup();
        this.isChangingPassword = false;
      },
      error: (err) => {
        console.error('Şifre değiştirme hatası:', err);
        const errorMessage =
          err?.error?.message || this.t('USER_PROFILE.CHANGE_PASSWORD.ERROR');
        notify(errorMessage, 'error', 5000);
        this.isChangingPassword = false;
      },
    });

    e.preventDefault();
  }
}
