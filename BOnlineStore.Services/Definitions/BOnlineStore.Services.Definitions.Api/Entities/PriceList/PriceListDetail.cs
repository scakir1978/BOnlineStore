using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Fiyat listesi detay entity.
    /// </summary>
    public class PriceListDetail : IdEntity, IAggregateRoot
    {
        /// <summary>
        /// Fiyat listesi id
        /// </summary>
        public string? PriceListId { get; private set; }

        /// <summary>
        /// Model Id
        /// </summary>
        public string? ModelId { get; private set; }

        /// <summary>
        /// Renk Id
        /// </summary>
        public string? ColorId { get; private set; }

        /// <summary>
        /// Cam türü id
        /// </summary>
        public string? GlassId { get; private set; }

        /// <summary>
        /// İlk en-1  ölçüsü
        /// </summary>
        public decimal? FirstWidth01 { get; private set; }

        /// <summary>
        /// Son en-1  ölçüsü
        /// </summary>
        public decimal? LastWidth01 { get; private set; }

        /// <summary>
        /// İlk en-2  ölçüsü
        /// </summary>
        public decimal? FirstWidth02 { get; private set; }

        /// <summary>
        /// Son en-2  ölçüsü
        /// </summary>
        public decimal? LastWidth02 { get; private set; }

        /// <summary>
        /// İlk yükseklik  ölçüsü
        /// </summary>
        public decimal? FirstHeight { get; private set; }

        /// <summary>
        /// Son yükseklik  ölçüsü
        /// </summary>
        public decimal? LastHeight { get; private set; }

        /// <summary>
        /// Satış Fiyatı
        /// </summary>
        public decimal? SalePrice { get; private set; }

        /// <summary>
        /// Döviz kodu id
        /// </summary>
        public string? CurrencyId { get; private set; }

        /// <summary>
        /// Ölçü farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListMeasurementDifference>? PriceListMeasurementDifferences { get; private set; }

        /// <summary>
        /// Cam deseni farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListGlassDifference>? PriceListGlassDifferences { get; private set; }

        /// <summary>
        /// Renk farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListColorDifference>? PriceListColorDifferences { get; private set; }

        public PriceListDetail() : base()
        { }

        public PriceListDetail(
            string id, string? priceListId = null, string? modelId = null, string? colorId = null,
            string? glassId = null, decimal? firstWidth01 = null, decimal? lastWidth01 = null,
            decimal? firstWidth02 = null, decimal? lastWidth02 = null, decimal? firstHeight = null,
            decimal? lastHeight = null, decimal? salePrice = null, string? currencyId = null,
            List<PriceListMeasurementDifference>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifference>? priceListGlassDifferences = null,
            List<PriceListColorDifference>? priceListColorDifferences = null) : base(id)
        {
            PriceListId = priceListId;
            ModelId = modelId;
            ColorId = colorId;
            GlassId = glassId;
            FirstWidth01 = firstWidth01;
            LastWidth01 = lastWidth01;
            FirstWidth02 = firstWidth02;
            LastWidth02 = lastWidth02;
            FirstHeight = firstHeight;
            LastHeight = lastHeight;
            SalePrice = salePrice;
            CurrencyId = currencyId;
            PriceListMeasurementDifferences = priceListMeasurementDifferences;
            PriceListGlassDifferences = priceListGlassDifferences;
            PriceListColorDifferences = priceListColorDifferences;
        }

        public void UpdatePriceListDetail(string? modelId = null, string? colorId = null,
            string? glassId = null, decimal? firstWidth01 = null, decimal? lastWidth01 = null,
            decimal? firstWidth02 = null, decimal? lastWidth02 = null, decimal? firstHeight = null,
            decimal? lastHeight = null, decimal? salePrice = null, string? currencyId = null,
            List<PriceListMeasurementDifference>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifference>? priceListGlassDifferences = null,
            List<PriceListColorDifference>? priceListColorDifferences = null)
        {
            ModelId = modelId;
            ColorId = colorId;
            GlassId = glassId;
            FirstWidth01 = firstWidth01;
            LastWidth01 = lastWidth01;
            FirstWidth02 = firstWidth02;
            LastWidth02 = lastWidth02;
            FirstHeight = firstHeight;
            LastHeight = lastHeight;
            SalePrice = salePrice;
            CurrencyId = currencyId;
            PriceListMeasurementDifferences = priceListMeasurementDifferences;
            PriceListGlassDifferences = priceListGlassDifferences;
            PriceListColorDifferences = priceListColorDifferences;
        }

    }
}
