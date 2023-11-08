using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montaj fiyatı
    /// </summary>
    public class AssemblyPriceDto : EntityDto
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

        public AssemblyPriceDto(string id, string? regionId = null, string? glassId = null,
            decimal? dealerPrice = null, decimal? assemblerPrice = null)
        {
            Id = id;
            RegionId = regionId;
            GlassId = glassId;
            DealerPrice = dealerPrice;
            AssemblerPrice = assemblerPrice;
        }
    }
}
