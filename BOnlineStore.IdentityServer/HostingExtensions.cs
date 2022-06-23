using Duende.IdentityServer;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.Services;
using BOnlineStore.IdentityServer.Business;
using System.Reflection;
using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;

namespace BOnlineStore.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITenantService, TenantManager>();

        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        builder.Services
            .AddIdentityServer(options =>
            {
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
            /*.AddInMemoryIdentityResources(Config.IdentityResources)            
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryApiResources(Config.ApiResources)*/
            .AddDeveloperSigningCredential()            
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });
        
        IMapper mapper = MappingConfigrations.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);                

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}