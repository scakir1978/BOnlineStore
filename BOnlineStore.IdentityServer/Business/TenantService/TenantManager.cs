using AutoMapper;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public class TenantManager : ITenantService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public TenantManager(ApplicationDbContext context, IMapper mapper, IStringLocalizer<Language> stringLocalizer)
        {
            _context = context;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<TenantDto> CreateAsync(TenantCreateDto tenantDto)
        {
            var existingTenant = FindByName(tenantDto.Name);
            if (existingTenant != null)
                throw new Exception(_stringLocalizer[IdentityServerKeys.TenantAlreadyExists]);

            var result = await _context.Tenant.AddAsync(_mapper.Map<Tenant>(tenantDto));

            await _context.SaveChangesAsync();

            return _mapper.Map<TenantDto>(result.Entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingTenant = _context.Tenant.FirstOrDefault(x => x.Id == id);
            if (existingTenant == null)
                throw new Exception(_stringLocalizer[IdentityServerKeys.TenantNotFoundForDelete]);

            _context.Tenant.Remove(existingTenant);
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
            var existingTenant = _context.Tenant.FirstOrDefault(x => x.Id == tenantDto.Id);
            if (existingTenant == null)
                throw new Exception(_stringLocalizer[IdentityServerKeys.TenantNotFoundForUpdate]);

            // Map the update values to the existing tracked entity
            _mapper.Map(tenantDto, existingTenant);
            existingTenant.UpdateDateTime = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<TenantDto>(existingTenant);
        }
    }
}
