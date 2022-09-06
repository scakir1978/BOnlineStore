using System.Security.Claims;
using IdentityModel;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using BOnlineStore.Shared;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.Shared.Constansts;

namespace BOnlineStore.IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            #region ConfigurationDbContext migration çalıştırılır ve intial kayıtlar eklenir.

            var configrationDbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            configrationDbContext.Database.Migrate();

            if (!configrationDbContext.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    configrationDbContext.Clients.Add(client.ToEntity());
                }
            }

            if (!configrationDbContext.ApiResources.Any())
            {
                foreach (var apiResource in Config.ApiResources)
                {
                    configrationDbContext.ApiResources.Add(apiResource.ToEntity());
                }
            }

            if (!configrationDbContext.ApiScopes.Any())
            {
                foreach (var apiScopes in Config.ApiScopes)
                {
                    configrationDbContext.ApiScopes.Add(apiScopes.ToEntity());
                }
            }

            if (!configrationDbContext.IdentityResources.Any())
            {
                foreach (var identityResource in Config.IdentityResources)
                {
                    configrationDbContext.IdentityResources.Add(identityResource.ToEntity());
                }
            }

            configrationDbContext.SaveChanges();

            #endregion

            #region ApplicationDbContext için migration çalıştırılır ve intial kayıtlar eklenir.

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var tenantDto = AddDefaultTenant(scope);
            AddDefaultUser(scope, tenantDto);

            #endregion

            #region PersistedGrantDbContext migration çalıştırılır.
            var persistedGrantDbContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            persistedGrantDbContext.Database.Migrate();
            #endregion
        }
    }

    private static void AddDefaultUser(IServiceScope scope, TenantDto tenantDto)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!userManager.Users.Any())
        {

            var adminUser = new ApplicationUser
            {
                UserName = "scakir1978@hotmail.com",
                Email = "scakir1978@hotmail.com",
                Locale = GlobalConstants.english,
                TenantId = tenantDto.Id
            };

            var result = userManager.CreateAsync(adminUser, "Scag185489*").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("administrator created");

        }
    }

    private static TenantDto AddDefaultTenant(IServiceScope scope)
    {
        var tenantManager = scope.ServiceProvider.GetRequiredService<ITenantService>();

        if (!tenantManager.IsAnyTenantExist())
        {
            var tenantCreateDto = new TenantCreateDto
            {
                //Id = defaultTenantId,
                Name = "Console.Log Deneme Şirketi",
                CreateDateTime = DateTime.Now,
                Adress = new Shared.Models.Adress
                {
                    Adress1 = "Beyaz Köşk Caddesi No:36 D:26",
                    CountryName = "Türkiye",
                    StateOrCityName = "İstanbul",
                    CityOrCountyName = "Kartal",
                    DistrictName = "Petrol İş Mah."

                }
            };

            var tenant = tenantManager.CreateAsync(tenantCreateDto).Result;

            Log.Debug("Default tenant created");

            return tenant;

        }
        else
        {
            Log.Debug("Default tenant returned");
            return tenantManager.FindByName("Console.Log Deneme Şirketi");
        }
    }
}
