using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Dtos.Role
{
    /// <summary>
    /// Rol güncelleme isteði
    /// </summary>
    public class RoleUpdateDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
