using BOnlineStore.IdentityServer.Dtos.Role;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BOnlineStore.IdentityServer.Business.RoleService
{
    public interface IRoleService
    {
        Task<(RoleDto Role, IdentityResult Result)> CreateAsync(RoleCreateDto roleCreateDto);
        Task<(RoleDto Role, IdentityResult Result)> UpdateAsync(string roleId, RoleUpdateDto roleUpdateDto);
        Task<(RoleDto Role, IdentityResult Result)> DeleteAsync(string roleId);
        Task<(RoleDto Role, IdentityResult Result)> GetByIdAsync(string roleId);
        Task<(RoleDto Role, IdentityResult Result)> GetByNameAsync(string roleName);
        Task<List<RoleDto>> GetAllAsync();
    }
}
