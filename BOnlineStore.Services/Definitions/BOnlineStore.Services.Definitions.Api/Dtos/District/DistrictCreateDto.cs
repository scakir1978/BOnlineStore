using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Mahalle eklemek için kullanılan dto
    /// </summary>
    public class DistrictCreateDto : EntityDto
    {
        /// <summary>
        /// Mahalle kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Mahalle  adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// İlçe id
        /// </summary>
        public string? CountyId { get; set; }

        public DistrictCreateDto(string code, string name, string? countyId = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            CountyId = countyId;
        }
    }
}
