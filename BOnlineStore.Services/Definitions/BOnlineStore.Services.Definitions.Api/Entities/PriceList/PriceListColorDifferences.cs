using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Renk farklarından oluşan fiyat bilgileri
    /// </summary>
    public class PriceListColorDifference : IdEntity, IAggregateRoot
    {
        /// <summary>
        /// Renk id
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

        public PriceListColorDifference() : base()
        {
            ColorId = "";
            PercentDifference = 0;
            CurrencyDifference = 0;
            SalePrice = 0;
        }

        public PriceListColorDifference(string id, string? colorId = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null) : base(id)
        {
            ColorId = colorId;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }

        public void UpdatePriceListMeasurementDifference(string? colorId = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null)
        {
            ColorId = colorId;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }
    }
}
