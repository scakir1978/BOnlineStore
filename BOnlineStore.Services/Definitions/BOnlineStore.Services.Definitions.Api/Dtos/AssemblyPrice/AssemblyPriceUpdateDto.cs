using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class AssemblyPriceUpdateDto
    {
        /// <summary>
        /// Bölge Kodu
        /// </summary>
        public string? RegionId { get; private set; }

        /// <summary>
        /// Cam Türü Kodu
        /// </summary>
        public string? GlassId { get; private set; }

        /// <summary>
        /// Bayi fiyatı
        /// </summary>
        public decimal? DealerPrice { get; private set; }

        /// <summary>
        /// Montör fiyatı
        /// </summary>
        public decimal? AssemblerPrice { get; private set; }

        public AssemblyPriceUpdateDto(string? regionId = null, string? glassId = null,
            decimal? dealerPrice = null, decimal? assemblerPrice = null)
        {
            RegionId = regionId;
            GlassId = glassId;
            DealerPrice = dealerPrice;
            AssemblerPrice = assemblerPrice;
        }
    }
}
