namespace BOnlineStore.IdentityServer.Settings
{
    public interface IIdentityConfigSettings
    {
        string RedirectUri { get; set; }
        string AllowedCorsOrigin1 { get; set; }
        string AllowedCorsOrigin2 { get; set; }
        string PostLogoutRedirectUri { get; set; }
        string FrontChannelLogoutUri { get; set; }
        string MongoDBRedirectUri { get; set; }
        string MongoDBPostLogoutRedirectUri { get; set; } 

    }
}
