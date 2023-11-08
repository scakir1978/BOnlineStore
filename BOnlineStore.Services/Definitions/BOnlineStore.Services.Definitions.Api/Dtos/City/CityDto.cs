using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Şehir
    /// </summary>
    public class CityDto : EntityDto
    {
        /// <summary>
        /// Şehir/İl Kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Şehir/İl Adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Ülke Id
        /// </summary>        
        public string? CountryId { get; set; }

        /// <summary>
        /// Bölge Id
        /// </summary>
        public string? RegionId { get; set; }

        public CityDto(string id, string code, string name, string? regionId = null, string? countryId = null)
        {
            Id = id;
            Code = code;
            Name = name;
            RegionId = regionId;
            CountryId = countryId;
        }
    }
}
