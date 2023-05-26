using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Montaj fiyatları
    /// </summary>
    public class AssemblyPrice : Entity
    {
        /// <summary>
        /// Bölge kodu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RegionId { get; private set; }

        /// <summary>
        /// Cam türü kodu
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

        public AssemblyPrice() : base()
        {
            RegionId = "";
            GlassId = "";
            DealerPrice = 0;
            AssemblerPrice = 0;
        }

        public AssemblyPrice(Guid tenantId, string id, string? regionId = null, string? glassId = null, decimal? dealerPrice = null, decimal? assemblerPrice = null) : base(tenantId, id)
        {
            RegionId = regionId;
            GlassId = glassId;
            DealerPrice = dealerPrice;
            AssemblerPrice = assemblerPrice;
        }

        public void UpdateAssemblyPrice(string? regionId = null, string? glassId = null, decimal? dealerPrice = null, decimal? assemblerPrice = null)
        {
            RegionId = regionId;
            GlassId = glassId;
            DealerPrice = dealerPrice;
            AssemblerPrice = assemblerPrice;
        }
    }
}
