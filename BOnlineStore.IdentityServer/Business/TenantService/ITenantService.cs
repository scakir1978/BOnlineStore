using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Shared.Dtos;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    public interface ITenantService
    {
        Task<Response<List<TenantDto>>> GetAllAsync();
        Task<Response<TenantDto>> GetByIdAsync(Guid id);
        Task<Response<TenantDto>> GetByNameAsync(string name);
        Task<Response<bool>> IsAnyTenantExistAsync();
        Task<Response<TenantDto>> CreateAsync(TenantCreateDto tenant);   
        Task<Response<TenantDto>> UpdateAsync(Guid id, TenantUpdateDto tenant);
        Task<Response<TenantDto>> DeleteAsync(Guid id);
    }
}
