using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Fiyat listesi cam türü farkları
    /// </summary>
    public class PriceListGlassDifference : IdEntity, IAggregateRoot
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

        public PriceListGlassDifference() : base()
        {
            GlassId = "";
            PercentDifference = 0;
            CurrencyDifference = 0;
            SalePrice = 0;
        }

        public PriceListGlassDifference(string id, string? glassId = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null) : base(id)
        {
            GlassId = glassId;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }

        public void UpdatePriceListMeasurementDifference(string? glassId = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null)
        {
            GlassId = glassId;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }
    }
}
