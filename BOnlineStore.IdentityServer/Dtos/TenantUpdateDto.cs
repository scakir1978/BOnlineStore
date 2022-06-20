using BOnlineStore.Shared.Models;

namespace BOnlineStore.IdentityServer.Dtos
{
    public class TenantUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public TaxInformation TaxInformation { get; set; }
        
    }
}
