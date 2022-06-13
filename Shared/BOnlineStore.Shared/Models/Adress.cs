using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Models
{
    public class Adress
    {
        public string? Adress1 { get; set; }
        public string? Adress2 { get; set; }
        public string? CountryName { get; set; }
        public string? StateOrCityName { get; set; } //Eyalet veya Şehir adı
        public string? CityOrCountyName { get; set; } //Şehir veya İlçe adı
        public string? DistrictName { get; set; } //Mahalle
        public int PostalCode { get; set; }        

    }
}
