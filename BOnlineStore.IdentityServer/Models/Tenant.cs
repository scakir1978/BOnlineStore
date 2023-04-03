using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public Adress Adress { get; set; }
        public TaxInformation TaxInformation { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }

    }
}
