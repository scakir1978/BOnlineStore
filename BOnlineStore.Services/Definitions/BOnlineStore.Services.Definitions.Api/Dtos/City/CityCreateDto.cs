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
        public string? Code { get; private set; }

        /// <summary>
        /// Şehir/İl Adı
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Bölge Id
        /// </summary>        
        public string? RegionId { get; private set; }

        public CityCreateDto(string code, string name, string? regionId = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            RegionId = regionId;
        }
    }
}
