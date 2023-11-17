namespace BOnlineStore.Shared
{
    public class IdentityServerConstants
    {
        #region Api Resources Constants

        public const string ApiResourcesBFF = "bff";
        public const string ApiResourcesDefinitions = "definitions";
        public const string ApiResourcesProduction = "production";
        public const string ApiResourcesOrder = "order";
        public const string ApiResourcesGateway = "gateway";

        #endregion

        #region Api Scopes Constanst

        public const string ApiScopesBFFFullPermission = "bff_full_permission";
        public const string ApiScopesDefinitionsFullPermission = "definitions_full_permission";
        public const string ApiScopesProductionFullPermission = "production_full_permission";
        public const string ApiScopesOrderFullPermission = "order_full_permission";
        public const string ApiScopesGatewayFullPermission = "gateway_full_permission";
        public const string ApiScopesDefinitionsTenantId = "tenantId";
        public const string ApiScopesDefinitionsLocale = "locale";

        #endregion
    }
}