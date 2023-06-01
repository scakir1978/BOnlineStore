using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// İlçe
    /// </summary>
    public class CountyDto : EntityDto
    {
        /// <summary>
        /// İlçe kodu.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// İlçe adı.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Şehir Id
        /// </summary>        
        public string? CityId { get; private set; }

        public CountyDto(string id, string code, string name, string? cityId = null)
        {
            Id = id;
            Code = code;
            Name = name;
            CityId = cityId;
        }
    }
}
