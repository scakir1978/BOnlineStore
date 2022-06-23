using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
