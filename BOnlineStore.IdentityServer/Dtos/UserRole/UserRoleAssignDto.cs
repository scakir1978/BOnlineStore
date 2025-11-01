using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Dtos.UserRole
{
    /// <summary>
    /// Kullanýcýya rol atama isteði
    /// </summary>
    public class UserRoleAssignDto
    {
        /// <summary>
        /// Kullanýcý ID
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Rol ID
        /// </summary>
        [Required]
        public string RoleId { get; set; }
    }
}
