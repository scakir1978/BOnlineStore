using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public class TenantService : ITenantService
    {
        protected readonly ApplicationDbContext _context;

        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Tenant> CreateAsync(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tenant> Tenants()
        {
            return _context.Tenant.AsQueryable();
        }

        public Task<Tenant> UpdateAsync(Tenant tenant)
        {
            throw new NotImplementedException();
        }
    }
}
