using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public interface ITenantService
    {
        IQueryable<TenantDto> Tenants();
        Task<TenantDto> FindByIdAsync(Guid id);
        Task<TenantDto> FindByNameAsync(string name);
        Task<TenantDto> CreateAsync(TenantCreateDto tenant);   
        Task<TenantDto> UpdateAsync(TenantUpdateDto tenant);
        Task<bool> DeleteAsync(Guid id);

    }
}
