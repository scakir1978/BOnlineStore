using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Localization.Constants
{
    public class IdentityServerKeys
    {
        public const string TenantsFetchError = "TenantsFetchError";
        public const string TenantFetchError = "TenantFetchError";
        public const string TenantNotFound = "TenantNotFound";
        public const string TenantIdMismatch = "TenantIdMismatch";
        public const string TenantDeleteFailed = "TenantDeleteFailed";
        public const string TenantExistenceCheckError = "TenantExistenceCheckError";
        public const string TenantAlreadyExists = "TenantAlreadyExists";
        public const string TenantNotFoundForDelete = "TenantNotFoundForDelete";
        public const string TenantNotFoundForUpdate = "TenantNotFoundForUpdate";
        
        // User Management Keys
        public const string UserNotFound = "UserNotFound";
        public const string CreateUserError = "CreateUserError";
        public const string UpdateUserError = "UpdateUserError";
        public const string DeleteUserError = "DeleteUserError";
        public const string ChangePasswordError = "ChangePasswordError";
        public const string ResetPasswordError = "ResetPasswordError";
        public const string UserNotFoundById = "UserNotFoundById";
        public const string UserNotFoundByEmail = "UserNotFoundByEmail";
        public const string PasswordChangedSuccessfully = "PasswordChangedSuccessfully";
        public const string PasswordResetSuccessfully = "PasswordResetSuccessfully";

        // Logout Page Keys
        public const string LoggedOutTitle = "LoggedOutTitle";
        public const string LoggedOutThanks = "LoggedOutThanks";
        public const string LoginAgain = "LoginAgain";
    }
}
