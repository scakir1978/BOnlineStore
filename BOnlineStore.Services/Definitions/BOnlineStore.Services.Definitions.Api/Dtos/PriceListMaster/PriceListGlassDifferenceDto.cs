namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Fiyat listesi cam türü farkları
    /// </summary>
    public class PriceListGlassDifferenceDto
    {
        /// <summary>
        /// Ölçü
        /// </summary>
        public string? GlassId { get; private set; }

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