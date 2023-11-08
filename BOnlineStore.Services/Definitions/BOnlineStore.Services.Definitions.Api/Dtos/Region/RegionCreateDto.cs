using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Bölge eklemek için kullanılan dto.
    /// </summary>
    public class RegionCreateDto : EntityDto
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

        public RegionCreateDto(string code, string name, string? countryId = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            CountryId = countryId;
        }
    }
}
