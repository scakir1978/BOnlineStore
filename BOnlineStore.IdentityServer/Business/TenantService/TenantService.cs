using AutoMapper;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public class TenantService : ITenantService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public TenantService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<TenantDto> CreateAsync(TenantCreateDto tenantDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDto> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDto> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TenantDto> Tenants()
        {
            return _mapper.Map<IQueryable<TenantDto>>(_context.Tenant.AsQueryable());
        }

        public Task<TenantDto> UpdateAsync(TenantUpdateDto tenant)
        {
            throw new NotImplementedException();
        }
    }
}
