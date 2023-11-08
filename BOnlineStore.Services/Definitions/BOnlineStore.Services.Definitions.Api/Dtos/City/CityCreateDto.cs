using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Şehir eklemek için kullanılan dto.
    /// </summary>
    public class CityCreateDto : EntityDto
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

        public CityCreateDto(string code, string name, string? regionId = null, string? countryId = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            RegionId = regionId;
            CountryId = countryId;
        }
    }
}
