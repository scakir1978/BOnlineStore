using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.Shared.Models
{
    /// <summary>
    /// Adres bilgileri.
    /// </summary>
    [Owned]
    public class Adress
    {
        /// <summary>
        /// Adres-1.
        /// </summary>
        [StringLength(256)]
        public string? Adress1 { get; set; }

        /// <summary>
        /// Adres-2.
        /// </summary>
        [StringLength(256)]
        public string? Adress2 { get; set; }

        /// <summary>
        /// Ülke.
        /// </summary>
        [StringLength(256)]
        public string? CountryName { get; set; }

        /// <summary>
        /// Eyalet veya Şehir adı.
        /// </summary>
        [StringLength(256)]
        public string? StateOrCityName { get; set; }

        /// <summary>
        /// Şehir veya İlçe adı.
        /// </summary>
        [StringLength(256)]
        public string? CityOrCountyName { get; set; }

        /// <summary>
        /// Mahalle.
        /// </summary>
        [StringLength(256)]
        public string? DistrictName { get; set; }

        /// <summary>
        /// Posta kodu.
        /// </summary>
        public int PostalCode { get; set; }

    }
}
