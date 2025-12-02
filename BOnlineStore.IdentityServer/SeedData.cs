using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Settings;

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

            Config.ConfigureIdentityConfigSettings(app.Services.GetRequiredService<IIdentityConfigSettings>());

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

            // Varsayılan roller eklenir.
            AddDefaultRoles(scope);

            // Varsayılan tenant eklenir.
            var tenantDto = AddDefaultTenant(scope);

            // Varsayılan kullanıcı eklenir.
            AddDefaultUser(scope, tenantDto);

            #endregion

            #region PersistedGrantDbContext migration çalıştırılır.
            var persistedGrantDbContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            persistedGrantDbContext.Database.Migrate();
            #endregion
        }
    }

    /// <summary>
    /// Uygulama için gerekli olan varsayılan rollerin kimlik sisteminde oluşturulmasını sağlar.
    /// </summary>
    /// <remarks>Bu metot, önceden tanımlanmış rollerin varlığını kontrol eder ve eğer henüz mevcut
    /// değillerse onları oluşturur. Roller <see cref="IdentityServerConstants"/> sınıfında tanımlanmıştır.
    /// Rol oluşturma işlemi başarısız olursa bir hata fırlatılır.</remarks>
    /// <param name="scope"><see cref="RoleManager{T}"/> servisini çözümlemek için kullanılan <see cref="IServiceScope"/>.</param>
    /// <exception cref="Exception">Kimlik sistemindeki bir hata nedeniyle rol oluşturma işlemi başarısız olursa fırlatılır.</exception>
    private static void AddDefaultRoles(IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new List<IdentityRole>
        {
            new IdentityRole(IdentityServerConstants.RoleNameSuperUser),
            new IdentityRole(IdentityServerConstants.RoleNameAdmin)
        };

        var serverRoles = roleManager.Roles.ToList();

        foreach (var role in roles)
        {
            if (!serverRoles.Exists(r => r.Name == role.Name))
            {
                var result = roleManager.CreateAsync(role).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        Log.Debug("Default roles created");

    }

    /// <summary>
    /// Sistemde şu anda hiç kullanıcı yoksa varsayılan bir yönetici kullanıcı ekler.
    /// </summary>
    /// <remarks>Bu metot, önceden tanımlanmış kimlik bilgileri ve özelliklere sahip varsayılan bir yönetici kullanıcı
    /// oluşturur. Yalnızca, hiç kullanıcının bulunmasının beklenmediği başlatma veya kurulum süreçlerinde çağrılmalıdır.
    /// Eğer bir kullanıcı zaten mevcutsa, metot herhangi bir işlem yapmaz.</remarks>
    /// <param name="scope">Kullanıcı yöneticisi gibi bağımlılıkları çözümlemek için kullanılan <see cref="IServiceScope"/>.</param>
    /// <param name="tenantDto">Varsayılan kullanıcıyı belirli bir şirketle ilişkilendirmek için kullanılan şirket bilgisi.</param>
    /// <exception cref="Exception">Kullanıcı oluşturma süreci başarısız olursa fırlatılır. İstisna mesajı hata ile ilgili
    /// ayrıntıları içerir.</exception>
    private static async void AddDefaultUser(IServiceScope scope, TenantDto tenantDto)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!userManager.Users.Any())
        {

            var adminUser = new ApplicationUser
            {
                UserName = IdentityServerConstants.SuperUserEmail,
                Email = IdentityServerConstants.SuperUserEmail,
                Locale = IdentityServerConstants.turkish,
                Name = IdentityServerConstants.SuperUserName,
                FamilyName = IdentityServerConstants.SuperUserFamilyName,
                Gender = IdentityServerConstants.SuperUserGender,
                Birthdate = new DateTime(1978, 1, 9),
                Nickname = IdentityServerConstants.SuperUserNickname,
                PreferredUsername = IdentityServerConstants.SuperUserEmail,
                TenantId = tenantDto.Id
            };

            var result = userManager.CreateAsync(adminUser, IdentityServerConstants.SuperUserDefaultPassword).Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var roleResult = await userManager.AddToRoleAsync(adminUser, IdentityServerConstants.RoleNameSuperUser);

            if (!roleResult.Succeeded)
            {
                throw new Exception(roleResult.Errors.First().Description);
            }

            Log.Debug("administrator created");

        }
    }

    /// <summary>
    /// Sistemde varsayılan bir şirketin (tenant) bulunmasını sağlar. Eğer hiç şirket yoksa,
    /// önceden tanımlanmış değerlerle varsayılan bir şirket oluşturur. Eğer varsayılan bir şirket zaten mevcutsa,
    /// onu getirir ve döndürür.
    /// </summary>
    /// <remarks>Bu metot, asenkron metotlara senkron çağrılar kullanır; bu durum belirli ortamlarda
    /// potansiyel kilitlenmelere (deadlock) yol açabilir. Metodun, bu davranışın kabul edilebilir olduğu
    /// bir bağlamda kullanıldığından emin olun.</remarks>
    /// <param name="scope">Şirket yönetimi için gerekli servisleri çözümlemek amacıyla kullanılan <see cref="IServiceScope"/>.</param>
    /// <returns>Varsayılan şirketi temsil eden bir <see cref="TenantDto"/>. Şirket yeni oluşturulduysa,
    /// oluşturulan şirket döndürülür. Şirket zaten mevcutsa, mevcut şirket döndürülür.</returns>
    /// <exception cref="Exception">Varsayılan şirketin oluşturulması veya mevcut şirketin getirilmesi başarısız olursa fırlatılır.</exception>
    private static TenantDto AddDefaultTenant(IServiceScope scope)
    {
        var tenantManager = scope.ServiceProvider.GetRequiredService<ITenantService>();

        var existsResponse = tenantManager.IsAnyTenantExistAsync().Result;

        if (!existsResponse.IsSucceed || !existsResponse.Result)
        {
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Console.Log",
                CreateDateTime = DateTime.Now,
                Adress = new Adress
                {
                    Adress1 = "Beyaz Köşk Caddesi No:36 D:26",
                    CountryName = "Türkiye",
                    StateOrCityName = "İstanbul",
                    CityOrCountyName = "Kartal",
                    DistrictName = "Petrol İş Mah."

                }
            };

            try
            {
                var response = tenantManager.CreateAsync(tenantCreateDto).Result;
                if (response.IsSucceed)
                {
                    Log.Debug("Default tenant created");
                    return response.Result;
                }
                else
                {
                    throw new Exception($"Failed to create tenant: {string.Join(", ", response.Errors.Select(e => e.Message))}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating default tenant");
                throw;
            }
        }
        else
        {
            Log.Debug("Default tenant returned");
            var tenantResponse = tenantManager.GetByNameAsync("Console.Log").Result;
            if (tenantResponse.IsSucceed)
            {
                return tenantResponse.Result;
            }
            else
            {
                throw new Exception($"Failed to get tenant: {string.Join(", ", tenantResponse.Errors.Select(e => e.Message))}");
            }
        }
    }
}
