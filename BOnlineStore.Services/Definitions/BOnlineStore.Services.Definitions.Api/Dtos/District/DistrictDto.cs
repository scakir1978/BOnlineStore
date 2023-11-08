using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Mahalle dto
    /// </summary>
    public class DistrictDto : EntityDto
    {
        /// <summary>
        /// Mahalle kodu.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Mahalle adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// İlçe id
        /// </summary>
        public string? CountyId { get; set; }

        public DistrictDto(string id, string code, string name, string? countyId = null)
        {
            Id = id;
            Code = code;
            Name = name;
            CountyId = countyId;
        }
    }
}
