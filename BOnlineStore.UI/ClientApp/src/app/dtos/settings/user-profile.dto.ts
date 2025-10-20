export interface UserProfileDto {
  id: string;
  email: string;
  phoneNumber?: string;
  tenantId?: string;
  locale?: string;
  name?: string;
  familyName?: string;
  givenName?: string;
  middleName?: string;
  nickname?: string;
  preferredUsername?: string;
  profile?: string;
  picture?: string;
  website?: string;
  gender?: string;
  birthdate?: Date | string;
  zoneInfo?: string;
}

export interface UserProfileUpdateDto {
  id: string;
  email: string;
  phoneNumber?: string;
  locale?: string;
  name?: string;
  familyName?: string;
  givenName?: string;
  middleName?: string;
  nickname?: string;
  preferredUsername?: string;
  profile?: string;
  picture?: string;
  website?: string;
  gender?: string;
  birthdate?: Date | string;
  zoneInfo?: string;
}
