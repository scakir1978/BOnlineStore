using BOnlineStore.Shared;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BOnlineStore.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource(Shared.IdentityServerConstants.ApiResourcesDefinitions){ Scopes={ Shared.IdentityServerConstants.ApiScopesDefinitionsFullPermission}},
        new ApiResource(Shared.IdentityServerConstants.ApiResourcesProduction){ Scopes={ Shared.IdentityServerConstants.ApiScopesProductionFullPermission}},
        new ApiResource(Shared.IdentityServerConstants.ApiResourcesOrder){ Scopes={ Shared.IdentityServerConstants.ApiScopesOrderFullPermission}},
        new ApiResource(Shared.IdentityServerConstants.ApiResourcesGateway){ Scopes={ Shared.IdentityServerConstants.ApiScopesGatewayFullPermission}},
        new ApiResource(Duende.IdentityServer.IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(Shared.IdentityServerConstants.ApiScopesDefinitionsFullPermission),
            new ApiScope(Shared.IdentityServerConstants.ApiScopesProductionFullPermission),
            new ApiScope(Shared.IdentityServerConstants.ApiScopesOrderFullPermission),
            new ApiScope(Shared.IdentityServerConstants.ApiScopesGatewayFullPermission),
            new ApiScope(Duende.IdentityServer.IdentityServerConstants.LocalApi.ScopeName),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "AngularClient",
                ClientName = "Angular Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:4200/pages/callback" },
                AllowedCorsOrigins={ "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200/pages/callout" },
                FrontChannelLogoutUri = "http://localhost:4200/pages/callout",
                AllowedScopes =
                {
                    //BOnlineStoreIdentityServerConstants.ApiScopesDefinitionsTenantId, 
                    Shared.IdentityServerConstants.ApiScopesDefinitionsFullPermission,
                    Shared.IdentityServerConstants.ApiScopesProductionFullPermission,
                    Shared.IdentityServerConstants.ApiScopesOrderFullPermission,
                    Shared.IdentityServerConstants.ApiScopesGatewayFullPermission,
                    Duende.IdentityServer.IdentityServerConstants.LocalApi.ScopeName,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess
                },
                AllowOfflineAccess = true,
                AccessTokenLifetime = ((60 * 60) * 6), // 6 saat
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AbsoluteRefreshTokenLifetime = (((60 * 60) * 24) * 5 ), //5 gün                
            }
        };
}
