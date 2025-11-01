using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BOnlineStore.IdentityServer.Business.RoleService
{
    public interface IRoleService
    {
        Task<Response<RoleDto>> CreateAsync(RoleCreateDto roleCreateDto);
        Task<Response<RoleDto>> UpdateAsync(string roleId, RoleUpdateDto roleUpdateDto);
        Task<Response<RoleDto>> DeleteAsync(string roleId);
        Task<Response<RoleDto>> GetByIdAsync(string roleId);
        Task<Response<RoleDto>> GetByNameAsync(string roleName);
        Task<List<RoleDto>> GetAllAsync();
    }
}
