namespace BOnlineStore.IdentityServer.Settings
{
    public class IdentityConfigSettings : IIdentityConfigSettings
    {
        public string RedirectUri { get; set; }
        public string AllowedCorsOrigin1 { get; set; }
        public string AllowedCorsOrigin2 { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string FrontChannelLogoutUri { get; set; }
    }
}
