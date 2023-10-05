using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Ölçü farklarından oluşan fiyat bilgileri
    /// </summary>
    public class PriceListColorDifferenceDto : EntityDto
    {
        /// <summary>
        /// Ölçü
        /// </summary>
        public string? ColorId { get; set; }

        /// <summary>
        /// Yüzdelik fark
        /// </summary>
        public decimal? PercentDifference { get; set; }

        /// <summary>
        /// Parasal fark
        /// </summary>
        public decimal? CurrencyDifference { get; set; }

        /// <summary>
        /// Satış Fiyatı
        /// </summary>
        public decimal? SalePrice { get; set; }
    }
}