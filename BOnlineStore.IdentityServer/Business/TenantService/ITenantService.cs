using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public interface ITenantService
    {
        IQueryable<Tenant> Tenants();
        Task<Tenant> FindByIdAsync(Guid id);
        Task<Tenant> FindByNameAsync(string name);
        Task<Tenant> CreateAsync(Tenant tenant);   
        Task<Tenant> UpdateAsync(Tenant tenant);
        Task<Tenant> DeleteAsync(Guid id);

    }
}
