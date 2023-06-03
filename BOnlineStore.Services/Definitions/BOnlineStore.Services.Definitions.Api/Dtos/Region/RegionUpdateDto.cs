namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Bölge güncellemek için kullanılan dto.
    /// </summary>
    public class RegionUpdateDto
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
        public string? CountryId { get; private set; }

        public RegionUpdateDto(string? code, string? name, string? countryId = null)
        {
            Code = code;
            Name = name;
            CountryId = countryId;
        }
    }
}
