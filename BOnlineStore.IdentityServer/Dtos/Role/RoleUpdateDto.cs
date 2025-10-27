using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Dtos.Role
{
    /// <summary>
    /// Rol güncelleme isteði
    /// </summary>
    public class RoleUpdateDto
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
