using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class AssemblyPriceCreateDto : EntityDto
    {
        /// <summary>
        /// Bölge Kodu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RegionId { get; private set; }

        /// <summary>
        /// Cam Türü Kodu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GlassId { get; private set; }

        /// <summary>
        /// Bayi fiyatı
        /// </summary>
        public decimal? DealerPrice { get; private set; }

        /// <summary>
        /// Montör fiyatı
        /// </summary>
        public decimal? AssemblerPrice { get; private set; }

        public AssemblyPriceCreateDto(string? regionId = null, string? glassId = null, decimal? dealerPrice = null,
                                      decimal? assemblerPrice = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            RegionId = regionId;
            GlassId = glassId;
            DealerPrice = dealerPrice;
            AssemblerPrice = assemblerPrice;
        }
    }
}
