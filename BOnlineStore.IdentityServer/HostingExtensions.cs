using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BOnlineStore.IdentityServer.Business;
using System.Reflection;
using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.HttpOverrides;

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

        builder.Services.AddRazorPages();



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

        builder.Services.AddAuthentication();

        IMapper mapper = MappingConfigrations.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCorsPolicy", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

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

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseIdentityServer();
        app.UseAuthorization();



        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}