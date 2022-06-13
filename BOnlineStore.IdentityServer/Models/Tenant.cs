using BOnlineStore.Shared.Models;

namespace BOnlineStore.IdentityServer.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
        public TaxInformation TaxInformation { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }

    }
}
