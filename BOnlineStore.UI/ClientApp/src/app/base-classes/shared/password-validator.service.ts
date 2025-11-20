import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PasswordValidatorService {
  /**
   * Validates password with medium complexity requirements:
   * - Minimum 8 characters
   * - At least 1 lowercase letter
   * - At least 1 uppercase letter
   * - At least 1 digit
   * - At least 1 special character
   *
   * @param password The password to validate
   * @returns true if password meets all requirements, false otherwise
   */
  validatePasswordStrength(password: string | null | undefined): boolean {
    if (!password) {
      return false;
    }

    return (
      password.length >= 8 &&
      /[a-z]/.test(password) &&
      /[A-Z]/.test(password) &&
      /[0-9]/.test(password) &&
      /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)
    );
  }

  /**
   * Gets a localized error message for password validation failures
   * @param translateService The translation service instance
   * @returns Error message string
   */
  getPasswordRequirementsMessage(translateService: any): string {
    return translateService.instant('PASSWORD_REQUIREMENTS');
  }
}
