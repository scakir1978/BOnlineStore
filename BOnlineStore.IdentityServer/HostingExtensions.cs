using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BOnlineStore.IdentityServer.Business;
using System.Reflection;
using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.HttpOverrides;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BOnlineStore.Shared.Constansts;

namespace BOnlineStore.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<IdentityConfigSettings>(builder.Configuration.GetSection(nameof(IdentityConfigSettings)));

        builder.Services.AddSingleton<IIdentityConfigSettings>(sp =>
        {
            return sp.GetRequiredService<IOptions<IdentityConfigSettings>>().Value;
        });

        builder.Services.AddScoped<ITenantService, TenantManager>();
        builder.Services.AddScoped<IUserService, UserManager>();

        // Localization configuration
        builder.Services.AddLocalization();

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo(GlobalConstants.turkish),
                new CultureInfo(GlobalConstants.english)
           };

            options.DefaultRequestCulture = new RequestCulture(GlobalConstants.turkish);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            // Accept language from Accept-Language header, query string, or cookie
            options.RequestCultureProviders = new List<IRequestCultureProvider>
           {
               new QueryStringRequestCultureProvider{
                   QueryStringKey = "culture",
                   UIQueryStringKey = "ui-culture"
               },
               new CookieRequestCultureProvider{ CookieName = "localeserver" },
               new AcceptLanguageHeaderRequestCultureProvider()
           };
        });

        builder.Services.AddRazorPages();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        var builderIdentity = builder.Services
            .AddIdentityServer(options =>
            {
                options.IssuerUri = builder.Configuration.GetValue<string>("IdentityServerUrl");
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
      {
          options.ConfigureDbContext = c =>
   {
       c.UseSqlServer
(
builder.Configuration.GetConnectionString("DefaultConnection"),
sqloptions => sqloptions.MigrationsAssembly(assemblyName)
);
   };

      })
      .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = c =>
                    {
                        c.UseSqlServer
                  (
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                               sqloptions => sqloptions.MigrationsAssembly(assemblyName)
            );
                    };

            })
       .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>();

        if (builder.Configuration[IdentityServerConstants.IdentityRunningMode] == "docker")
        {
            var certName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/bonlinestore.pfx";
            Log.Information(File.Exists(certName) == true ? $"Certificate status: {certName} found." : $"Certificate status: {certName} NOT FOUND!!!");

            builderIdentity.AddSigningCredential(new X509Certificate2(certName, "Scag185489"));

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80);
                options.ListenAnyIP(443, listenOptions =>
              {
                  listenOptions.UseConnectionLogging();
                  listenOptions.UseHttps(certName, "Scag185489");
              });
            });
        }
        else
            builderIdentity.AddDeveloperSigningCredential();

        // Add LocalApi authentication - this allows IdentityServer to protect its own API endpoints
        builder.Services.AddLocalApiAuthentication();

        builder.Services.AddAuthentication();

        IMapper mapper = MappingConfigrations.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
          {
              options.ForwardedHeaders =
                  ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

              options.KnownNetworks.Clear();
              options.KnownProxies.Clear();
          });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseForwardedHeaders();

        // Persist culture from query (culture, ui-culture, ui_locales) into the localization cookie as early as possible
        app.Use(async (context, next) =>
        {
            var cultureFromQuery = context.Request.Query["culture"].FirstOrDefault()
                ?? context.Request.Query["ui-culture"].FirstOrDefault()
                ?? context.Request.Query["ui_locales"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(cultureFromQuery))
            {
                try
                {
                    var culture = new CultureInfo(cultureFromQuery);
                    var requestCulture = new RequestCulture(culture, culture);
                    context.Response.Cookies.Append(
                         "localeserver",
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

        // Add request localization middleware
        app.UseRequestLocalization();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseIdentityServer();

        // Important: Add authentication before authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        app.MapControllers();

        return app;
    }
}