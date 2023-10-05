using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    ///Fiyat listesi detay entity.
    /// </summary>
    public class PriceListDetailDto : EntityDto
    {
        /// <summary>
        /// Fiyat listesi id
        /// </summary>
        public string? PriceListId { get; set; }

        /// <summary>
        /// Model Id
        /// </summary>
        public string? ModelId { get; set; }

        /// <summary>
        /// Renk Id
        /// </summary>
        public string? ColorId { get; set; }

        /// <summary>
        /// Cam türü id
        /// </summary>
        public string? GlassId { get; set; }

        /// <summary>
        /// İlk en-1  ölçüsü
        /// </summary>
        public decimal? FirstWidth01 { get; set; }

        /// <summary>
        /// Son en-1  ölçüsü
        /// </summary>
        public decimal? LastWidth01 { get; set; }

        /// <summary>
        /// İlk en-2  ölçüsü
        /// </summary>
        public decimal? FirstWidth02 { get; set; }

        /// <summary>
        /// Son en-2  ölçüsü
        /// </summary>
        public decimal? LastWidth02 { get; set; }

        /// <summary>
        /// İlk yükseklik  ölçüsü
        /// </summary>
        public decimal? FirstHeight { get; set; }

        /// <summary>
        /// Son yükseklik  ölçüsü
        /// </summary>
        public decimal? LastHeight { get; set; }

        /// <summary>
        /// Satış Fiyatı
        /// </summary>
        public decimal? SalePrice { get; set; }

        /// <summary>
        /// Döviz kodu id
        /// </summary>
        public string? CurrencyId { get; set; }

        /// <summary>
        /// Ölçü farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListMeasurementDifferenceDto>? PriceListMeasurementDifferences { get; set; }

        /// <summary>
        /// Cam deseni farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListGlassDifferenceDto>? PriceListGlassDifferences { get; set; }

        /// <summary>
        /// Renk farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListColorDifferenceDto>? PriceListColorDifferences { get; set; }
    }
}