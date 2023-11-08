using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Bölge
    /// </summary>
    public class RegionDto : EntityDto
    {
        /// <summary>
        /// Bölge kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Bölge adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Ülke Id.
        /// </summary>
        public string? CountryId { get; set; }

        public RegionDto(string id, string code, string name, string? countryId = null)
        {
            Id = id;
            Code = code;
            Name = name;
            CountryId = countryId;
        }
    }
}
