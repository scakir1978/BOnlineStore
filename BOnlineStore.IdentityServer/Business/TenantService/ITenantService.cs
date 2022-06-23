using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public interface ITenantService
    {
        IQueryable<TenantDto> Tenants();
        TenantDto FindById(Guid id);
        TenantDto FindByName(string name);
        bool IsAnyTenantExist();
        Task<TenantDto> CreateAsync(TenantCreateDto tenant);   
        Task<TenantDto> UpdateAsync(TenantUpdateDto tenant);
        Task<bool> DeleteAsync(Guid id);

    }
}
