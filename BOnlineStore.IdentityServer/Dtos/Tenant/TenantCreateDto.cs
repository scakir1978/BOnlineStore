using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Dtos
{
    public class TenantCreateDto
    {
        /// <summary>
        /// Firma kimliği
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Firma adı
        /// </summary>
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
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Güncellenme tarihi
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
    }
}
