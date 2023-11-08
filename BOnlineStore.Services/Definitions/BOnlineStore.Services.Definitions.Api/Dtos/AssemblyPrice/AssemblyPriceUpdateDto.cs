using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// /// Montaj fiyatı güncellemek için kullanılan dto
    /// </summary>
    public class AssemblyPriceUpdateDto
    {
        /// <summary>
        /// Bölge Kodu
        /// </summary>
        public string? RegionId { get; set; }

        /// <summary>
        /// Cam Türü Kodu
        /// </summary>
        public string? GlassId { get; set; }

        /// <summary>
        /// Bayi fiyatı
        /// </summary>
        public decimal? DealerPrice { get; set; }

        /// <summary>
        /// Montör fiyatı
        /// </summary>
        public decimal? AssemblerPrice { get; set; }

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
