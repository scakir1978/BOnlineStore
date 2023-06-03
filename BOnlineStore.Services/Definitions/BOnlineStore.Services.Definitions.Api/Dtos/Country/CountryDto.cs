using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Ülke
    /// </summary>
    public class CountryDto : EntityDto
    {
        /// <summary>
        /// Ülke kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Ülke adı.
        /// </summary>
        public string? Name { get; set; }

        public CountryDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
