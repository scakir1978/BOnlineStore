using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using BOnlineStore.IdentityServer.Settings;

namespace BOnlineStore.IdentityServer.Extensions
{
    public static class RequestLocalizationExtensions
    {
        // Adds middleware to persist culture from query (culture, ui-culture, ui_locales)
        // into the configured localization cookie before UseRequestLocalization runs.
        public static IApplicationBuilder UsePersistCultureFromQuery(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var cultureFromQuery = context.Request.Query[IdentityServerConstants.CultureQueryStringKey].FirstOrDefault()
                    ?? context.Request.Query[IdentityServerConstants.UICultureQueryStringKey].FirstOrDefault()
                    ?? context.Request.Query[IdentityServerConstants.UILocalesQueryStringKey].FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(cultureFromQuery))
                {
                    try
                    {
                        var culture = new CultureInfo(cultureFromQuery);
                        var requestCulture = new RequestCulture(culture, culture);
                        context.Response.Cookies.Append(
                             IdentityServerConstants.LocalizationCookieName,
                             CookieRequestCultureProvider.MakeCookieValue(requestCulture),
                             new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                         );
                    }
                    catch (CultureNotFoundException)
                    {
                        // ignore invalid culture
                    }
                }
                await next();
            });
        }
    }
}
