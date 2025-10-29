using BOnlineStore.IdentityServer.Dtos.UserRole;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Business.UserRoleService
{
    public interface IUserRoleService
    {
        Task<(UserRoleDto UserRole, IdentityResult Result)> AssignRoleToUserAsync(UserRoleAssignDto userRoleAssignDto);
        Task<(UserRoleDto UserRole, IdentityResult Result)> RemoveRoleFromUserAsync(string userId, string roleId);
        Task<List<UserRoleDto>> GetUserRolesAsync(string userId);
        Task<List<UserRoleDto>> GetAllUserRolesAsync();
    }
}
