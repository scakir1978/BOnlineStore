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
    public class Adress
    {
        [StringLength(256)]
        public string Adress1 { get; set; }

        [StringLength(256)]
        public string? Adress2 { get; set; }

        [StringLength(256)]
        public string? CountryName { get; set; }

        [StringLength(256)]
        public string? StateOrCityName { get; set; } //Eyalet veya Şehir adı

        [StringLength(256)]
        public string? CityOrCountyName { get; set; } //Şehir veya İlçe adı

        [StringLength(256)]
        public string? DistrictName { get; set; } //Mahalle        

        public int PostalCode { get; set; }        

    }
}
