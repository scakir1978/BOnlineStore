using AutoMapper;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public class TenantManager : ITenantService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public TenantManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TenantDto> CreateAsync(TenantCreateDto tenantDto)
        {
            var existingTenant = FindByName(tenantDto.Name);
            if (existingTenant != null)
                throw new Exception("Girilen şirket sistemde mevcut");

            var result = await _context.Tenant.AddAsync(_mapper.Map<Tenant>(tenantDto));

            await _context.SaveChangesAsync();

            return _mapper.Map<TenantDto>(result.Entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingTenant = FindById(id);
            if (existingTenant == null)
                throw new Exception("Silinecek şirket sistemde bulunamadı.");

            var tenant = _context.Tenant.Remove(_mapper.Map<Tenant>(existingTenant));
            return await _context.SaveChangesAsync() > 0;
        }

        public TenantDto FindById(Guid id)
        {
            return _mapper.Map<TenantDto>(_context.Tenant.Where(x => x.Id == id).FirstOrDefault());
        }

        public TenantDto FindByName(string name)
        {
            return _mapper.Map<TenantDto>(_context.Tenant.Where(x => x.Name == name).FirstOrDefault());
        }

        public IQueryable<TenantDto> Tenants()
        {
            return _mapper.ProjectTo<TenantDto>(_context.Tenant.AsQueryable());
        }

        public bool IsAnyTenantExist()
        {
            return _context.Tenant.Any();
        }

        public async Task<TenantDto> UpdateAsync(TenantUpdateDto tenantDto)
        {
            var existingTenant = FindById(tenantDto.Id);
            if (existingTenant == null)
                throw new Exception("Güncellenecek şirket sistemde bulunamadı");

            var result = _context.Tenant.Update(_mapper.Map<Tenant>(tenantDto));
            await _context.SaveChangesAsync();
            return _mapper.Map<TenantDto>(result.Entity);
        }
    }
}
