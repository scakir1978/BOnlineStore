using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.Shared.Models
{
    [Owned]
    public class TaxInformation
    {
        [StringLength(256)]
        public string? TaxAdministration { get; set; } //Vergi dairesi

        [StringLength(256)]
        public string? TaxNumber { get; set; } //Vergi numarası

    }
}
