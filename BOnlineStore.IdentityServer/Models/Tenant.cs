using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Models
{
    public class Tenant
    {
        /// <summary>
        /// Firma kimliği
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Firma adı
        /// </summary>
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Adres bilgileri
        /// </summary>
        public Adress Adress { get; set; }

        /// <summary>
        /// Vergi bilgileri
        /// </summary>
        public TaxInformation TaxInformation { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// Güncellenme tarihi
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Kiracıya bağlı kullanıcılar
        /// </summary>
        public virtual List<ApplicationUser> Users { get; set; }

    }
}
