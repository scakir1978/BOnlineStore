namespace BOnlineStore.IdentityServer.Dtos.UserRole
{
    /// <summary>
    /// Kullanýcý-Rol bilgileri
    /// </summary>
    public class UserRoleDto
    {
        /// <summary>
        /// Kullanýcý ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Rol ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Kullanýcý adý
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rol adý
        /// </summary>
        public string RoleName { get; set; }
    }
}
