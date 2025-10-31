using BOnlineStore.IdentityServer.Dtos.UserRole;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Business.UserRoleService
{
    public interface IUserRoleService
    {
        Task<Response<UserRoleDto>> AssignRoleToUserAsync(UserRoleAssignDto userRoleAssignDto);
        Task<Response<UserRoleDto>> RemoveRoleFromUserAsync(string userId, string roleId);
        Task<Response<List<UserRoleDto>>> GetUserRolesAsync(string userId);
        Task<Response<List<UserRoleDto>>> GetAllUserRolesAsync();
    }
}
