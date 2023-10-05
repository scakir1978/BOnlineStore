using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Fiyat listesi ölçü farkları
    /// </summary>
    public class PriceListMeasurementDifferenceDto : EntityDto
    {
        /// <summary>
        /// Ölçü
        /// </summary>
        public decimal? Measurement { get; set; }

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