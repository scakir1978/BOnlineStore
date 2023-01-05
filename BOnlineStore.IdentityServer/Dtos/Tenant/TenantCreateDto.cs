using BOnlineStore.Shared.Models;

namespace BOnlineStore.IdentityServer.Dtos
{
    public class TenantCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public TaxInformation TaxInformation { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
