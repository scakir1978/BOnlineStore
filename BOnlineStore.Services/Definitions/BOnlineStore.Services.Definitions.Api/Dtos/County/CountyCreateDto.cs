using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// İlçe
    /// </summary>
    public class CountyCreateDto : EntityDto
    {
        /// <summary>
        /// İlçe kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// İlçe adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Şehir Id
        /// </summary>        
        public string? CityId { get; private set; }

        public CountyCreateDto(string code, string name, string? cityId = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            CityId = cityId;
        }
    }
}
