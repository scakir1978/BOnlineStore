﻿using BOnlineStore.IdentityServer.Settings;
using Duende.IdentityServer.Models;

namespace BOnlineStore.IdentityServer;

public static class Config
{
    private static IIdentityConfigSettings _identityConfigSettings;
    public static void ConfigureIdentityConfigSettings(IIdentityConfigSettings identityConfigSettings)
    {
        _identityConfigSettings = identityConfigSettings;
    }

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource(IdentityServerConstants.ApiResourcesDefinitions){ Scopes={ IdentityServerConstants.ApiScopesDefinitionsFullPermission}},
        new ApiResource(IdentityServerConstants.ApiResourcesProduction){ Scopes={ IdentityServerConstants.ApiScopesProductionFullPermission}},
        new ApiResource(IdentityServerConstants.ApiResourcesOrder){ Scopes={ IdentityServerConstants.ApiScopesOrderFullPermission}},
        new ApiResource(IdentityServerConstants.ApiResourcesBFF){ Scopes={ IdentityServerConstants.ApiScopesBFFFullPermission}},
        new ApiResource(IdentityServerConstants.ApiResourcesGateway){ Scopes={ IdentityServerConstants.ApiScopesGatewayFullPermission}},
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
            new ApiScope(IdentityServerConstants.ApiScopesDefinitionsFullPermission),
            new ApiScope(IdentityServerConstants.ApiScopesProductionFullPermission),
            new ApiScope(IdentityServerConstants.ApiScopesOrderFullPermission),
            new ApiScope(IdentityServerConstants.ApiScopesGatewayFullPermission),
            new ApiScope(IdentityServerConstants.ApiScopesBFFFullPermission),
            new ApiScope(IdentityServerConstants.ApiScopesMongoDBFullPermission),
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
                RedirectUris = { _identityConfigSettings.RedirectUri },
                AllowedCorsOrigins={ _identityConfigSettings.AllowedCorsOrigin1, _identityConfigSettings.AllowedCorsOrigin2 },
                PostLogoutRedirectUris = { _identityConfigSettings.PostLogoutRedirectUri },
                FrontChannelLogoutUri = _identityConfigSettings.FrontChannelLogoutUri,
                AllowedScopes =
                {
                    IdentityServerConstants.ApiScopesDefinitionsFullPermission,
                    IdentityServerConstants.ApiScopesProductionFullPermission,
                    IdentityServerConstants.ApiScopesOrderFullPermission,
                    IdentityServerConstants.ApiScopesGatewayFullPermission,
                    IdentityServerConstants.ApiScopesBFFFullPermission,
                    Duende.IdentityServer.IdentityServerConstants.LocalApi.ScopeName,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OfflineAccess
                },
                AllowOfflineAccess = true,
                AccessTokenLifetime = ((60 * 60) * 6), // 6 saat
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AbsoluteRefreshTokenLifetime = (((60 * 60) * 24) * 5 ), //5 gün                
            },
            new Client
            {
                ClientId = "MongoDBClient",
                ClientName = "MongoDB Client",
                RequireClientSecret = true,
                ClientSecrets = { new Secret("cyxtLPb0aKRMzqYgOCRuQURt3kV1DwZwNDuHnjErdtidTVHRzD6BhcXfStuBPEW0sbb4LQ==".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RedirectUris = { _identityConfigSettings.RedirectUri },
                PostLogoutRedirectUris = { _identityConfigSettings.PostLogoutRedirectUri },
                AllowedScopes =
                {                    
                    IdentityServerConstants.ApiScopesMongoDBFullPermission
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}
