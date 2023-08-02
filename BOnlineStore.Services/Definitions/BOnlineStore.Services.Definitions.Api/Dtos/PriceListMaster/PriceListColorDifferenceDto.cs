namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Ölçü farklarından oluşan fiyat bilgileri
    /// </summary>
    public class PriceListColorDifferenceDto
    {
        /// <summary>
        /// Ölçü
        /// </summary>
        public string? ColorId { get; private set; }

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