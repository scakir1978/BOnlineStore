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
using BOnlineStore.IdentityServer.Extensions;
using BOnlineStore.IdentityServer.Business.RoleService;
using BOnlineStore.IdentityServer.Business.UserRoleService;

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

        // HttpContextAccessor ekle
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<ITenantService, TenantManager>();
        builder.Services.AddScoped<IUserService, UserManager>();
        builder.Services.AddScoped<IRoleService, RoleManager>();
        builder.Services.AddScoped<IUserRoleService, UserRoleManager>();

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
                   QueryStringKey = IdentityServerConstants.CultureQueryStringKey,
                   UIQueryStringKey = IdentityServerConstants.UICultureQueryStringKey
               },
               new CookieRequestCultureProvider{ CookieName = IdentityServerConstants.LocalizationCookieName },
               new AcceptLanguageHeaderRequestCultureProvider()
           };
        });

        builder.Services.AddRazorPages();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString(IdentityServerConstants.DefaultConnectionStringName)));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        var builderIdentity = builder.Services
            .AddIdentityServer(options =>
            {
                options.IssuerUri = builder.Configuration.GetValue<string>(IdentityServerConstants.ConfigKeyIdentityServerUrl);
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
                    builder.Configuration.GetConnectionString(IdentityServerConstants.DefaultConnectionStringName),
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
                    builder.Configuration.GetConnectionString(IdentityServerConstants.DefaultConnectionStringName),
                               sqloptions => sqloptions.MigrationsAssembly(assemblyName)
            );
                    };

            })
       .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>();

        if (builder.Configuration[IdentityServerConstants.IdentityRunningMode] == IdentityServerConstants.RunningModeDocker)
        {
            var certName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + IdentityServerConstants.SigningCertificateFile;
            Log.Information(File.Exists(certName) == true ? string.Format(IdentityServerConstants.CertFoundLogTemplate, certName) : string.Format(IdentityServerConstants.CertNotFoundLogTemplate, certName));

            builderIdentity.AddSigningCredential(new X509Certificate2(certName, IdentityServerConstants.SigningCertificatePassword));

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80);
                options.ListenAnyIP(443, listenOptions =>
              {
                  listenOptions.UseConnectionLogging();
                  listenOptions.UseHttps(certName, IdentityServerConstants.SigningCertificatePassword);
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

        // Persist culture from query params into cookie early
        app.UsePersistCultureFromQuery();

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