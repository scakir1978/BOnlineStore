using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montaj fiyatı eklemek için kullanılan dto
    /// </summary>
    public class AssemblyPriceCreateDto : EntityDto
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
