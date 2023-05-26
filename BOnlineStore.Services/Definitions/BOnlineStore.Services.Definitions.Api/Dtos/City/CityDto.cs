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
        public string Code { get; private set; }

        /// <summary>
        /// Şehir/İl Adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Bölge Id
        /// </summary>
        public string? RegionId { get; private set; }

        public CityDto(string id, string code, string name, string? regionId = null)
        {
            Id = id;
            Code = code;
            Name = name;
            RegionId = regionId;
        }
    }
}
