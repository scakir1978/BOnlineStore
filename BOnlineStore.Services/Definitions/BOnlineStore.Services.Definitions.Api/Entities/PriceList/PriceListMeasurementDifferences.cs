using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Ölçü farkları
    /// </summary>
    public class PriceListMeasurementDifference : IdEntity, IAggregateRoot
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

        public PriceListMeasurementDifference() : base()
        {
            Measurement = 0;
            PercentDifference = 0;
            CurrencyDifference = 0;
            SalePrice = 0;
        }

        public PriceListMeasurementDifference(string id, decimal? measurement = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null) : base(id)
        {
            Measurement = measurement;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }

        public void UpdatePriceListMeasurementDifference(decimal? measurement = null,
            decimal? percentDifference = null,
            decimal? currencyDifference = null,
            decimal? salePrice = null)
        {
            Measurement = measurement;
            PercentDifference = percentDifference;
            CurrencyDifference = currencyDifference;
            SalePrice = salePrice;
        }
    }
}
