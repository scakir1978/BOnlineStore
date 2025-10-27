using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Dtos.Role
{
 /// <summary>
 /// Yeni rol oluþturma isteði
 /// </summary>
 public class RoleCreateDto
 {
 [Required]
 [MaxLength(256)]
 public string Name { get; set; }
 }
}
