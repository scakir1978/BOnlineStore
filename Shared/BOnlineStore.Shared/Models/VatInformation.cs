using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.Shared.Models
{
    /// <summary>
    /// Vergi bilgileri
    /// </summary>
    [Owned]
    public class TaxInformation
    {
        /// <summary>
        /// Vergi dairesi.
        /// </summary>
        [StringLength(256)]
        public string? TaxAdministration { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(256)]
        public string? TaxNumber { get; set; }

    }
}
