import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import notify from 'devextreme/ui/notify';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationService } from 'app/core/services/auth.service';
import { UserProfileService } from 'app/settings/user-profile/user-profile.service';
import {
  UserProfileDto,
  UserProfileUpdateDto,
} from 'app/dtos/settings/user-profile.dto';

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

  // Gender options (labels localized)
  genderOptions: Array<{ value: string; text: string }> = [];

  // Locale options
  localeOptions: Array<{ value: string; text: string }> = [];

  maxBirthDate = new Date();

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private userProfileService: UserProfileService,
    private translate: TranslateService
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
  }

  ngOnInit(): void {
    // initialize localized option labels and react to language changes
    this.updateLocalizedOptions();
    this.translate.onLangChange.subscribe(() => this.updateLocalizedOptions());

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
  }

  loadUserProfile(): void {
    this.isLoading = true;
    this.userProfileService.getUserByEmail(this.userEmail).subscribe({
      next: (data) => {
        this.userProfile = data;
        if (this.userProfile?.birthdate) {
          this.userProfile.birthdate = new Date(this.userProfile.birthdate);
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

  saveUserProfile(): void {
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
  }

  cancelEdit(): void {
    this.loadUserProfile();
  }
}
