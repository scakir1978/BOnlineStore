namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Şehir
    /// </summary>
    public class CityUpdateDto
    {
        /// <summary>
        /// Şehir/İl Kodu
        /// </summary>
        public string? Code { get; private set; }

        /// <summary>
        /// Şehir/İl Adı
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Bölge Id
        /// </summary>        
        public string? RegionId { get; private set; }

        public CityUpdateDto(string? code, string? name, string? regionId = null)
        {
            Code = code;
            Name = name;
            RegionId = regionId;
        }
    }
}
