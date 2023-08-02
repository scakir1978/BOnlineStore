namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Ölçü farkları
    /// </summary>
    public class PriceListMeasurementDifferenceDto
    {
        /// <summary>
        /// Ölçü
        /// </summary>
        public decimal? Measurement { get; private set; }

        /// <summary>
        /// Yüzdelik fark
        /// </summary>
        public decimal? PercentDifference { get; private set; }

        /// <summary>
        /// Parasal fark
        /// </summary>
        public decimal? CurrencyDifference { get; private set; }

        /// <summary>
        /// Satış Fiyatı
        /// </summary>
        public decimal? SalePrice { get; private set; }
    }
}