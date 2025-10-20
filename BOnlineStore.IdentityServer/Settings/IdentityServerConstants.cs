namespace BOnlineStore.IdentityServer.Settings
{
    public class IdentityServerConstants
    {
        public const string IdentityRunningMode = "IdentityRunningMode";
        public const string english = "en-US";
        public const string turkish = "tr-TR";

        #region Api Resources Constants

        public const string ApiResourcesDefinitions = "definitions";
        public const string ApiResourcesBFF = "bff";
        public const string ApiResourcesProduction = "production";
        public const string ApiResourcesOrder = "order";
        public const string ApiResourcesGateway = "gateway";

        #endregion

        #region Api Scopes Constanst

        public const string ApiScopesDefinitionsFullPermission = "definitions_full_permission";
        public const string ApiScopesProductionFullPermission = "production_full_permission";
        public const string ApiScopesOrderFullPermission = "order_full_permission";
        public const string ApiScopesGatewayFullPermission = "gateway_full_permission";
        public const string ApiScopesBFFFullPermission = "bff_full_permission";        
        public const string ApiScopesMongoDBFullPermission = "mongodb_full_permission";

        #endregion

        #region Profile Claim Types Constants

        public const string ProfilClaimTypeTenantId = "tenantId";        
        public const string ProfilClaimTypeName = "name";
        public const string ProfilClaimTypeFamilyName = "family_name";
        public const string ProfilClaimTypeGivenName = "given_name";
        public const string ProfilClaimTypeMiddleName = "middle_name";
        public const string ProfilClaimTypeNickname = "nickname";
        public const string ProfilClaimTypePreferredUsername = "preferred_username";
        public const string ProfilClaimTypeProfile = "profile";
        public const string ProfilClaimTypePicture = "picture";
        public const string ProfilClaimTypeWebsite = "website";
        public const string ProfilClaimTypeGender = "gender";
        public const string ProfilClaimTypeBirthdate = "birthdate";
        public const string ProfilClaimTypeZoneInfo = "zoneinfo";
        public const string ProfilClaimTypeLocale = "locale";
        public const string ProfilClaimTypeUpdatedAt = "updated_at";
        public const string ProfilClaimTypeEmail = "email";
        public const string ProfilClaimTypeNormalizedEmail = "normalized_email";

        #endregion
    }
}
