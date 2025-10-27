using BOnlineStore.IdentityServer.Dtos.Role;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Business.RoleService
{
    public interface IRoleService
    {
        Task<(RoleDto Role, IdentityResult Result)> CreateAsync(RoleCreateDto roleCreateDto);
        Task<(RoleDto Role, IdentityResult Result)> UpdateAsync(RoleUpdateDto roleUpdateDto);
        Task<IdentityResult> DeleteAsync(string roleId);
        Task<RoleDto> GetByIdAsync(string roleId);
        Task<RoleDto> GetByNameAsync(string roleName);
        Task<List<RoleDto>> GetAllAsync();
    }
}
