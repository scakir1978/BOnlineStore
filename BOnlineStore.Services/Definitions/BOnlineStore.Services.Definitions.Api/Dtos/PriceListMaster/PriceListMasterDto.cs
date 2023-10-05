using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Fiyat listesi için kullanılan dto
    /// </summary>
    public class PriceListMasterDto : EntityDto
    {
        /// <summary>
        /// Fiyat listesi kodu.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Fiyatlistesi açıklaması veya adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi başlangıç tarihi
        /// </summary>
        public DateTime? FirstDate { get; set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

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

        /// <summary>
        /// Fiyat listesi detayları
        /// </summary>
        public List<PriceListDetailDto>? PriceListDetails { get; set; }

        public PriceListMasterDto(
            string id,
            string code,
            string name,
            DateTime? firstDate = null,
            DateTime? endDate = null,
            List<PriceListMeasurementDifferenceDto>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifferenceDto>? priceListGlassDifferences = null,
            List<PriceListColorDifferenceDto>? priceListColorDifferences = null,
            List<PriceListDetailDto>? priceListDetails = null)
        {
            Id = id;
            Code = code;
            Name = name;
            FirstDate = firstDate;
            EndDate = endDate;
            PriceListMeasurementDifferences = priceListMeasurementDifferences;
            PriceListGlassDifferences = priceListGlassDifferences;
            PriceListColorDifferences = priceListColorDifferences;
            PriceListDetails = priceListDetails;
        }
    }
}
