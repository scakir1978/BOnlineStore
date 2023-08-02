using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Fiyat listesi eklemek için kullanılan dto
    /// </summary>
    public class PriceListMasterCreateDto : EntityDto
    {
        /// <summary>
        /// Fiyat listesi kodu.
        /// </summary>
        public string? Code { get; private set; }

        /// <summary>
        /// Fiyatlistesi açıklaması veya adı
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi başlangıç tarihi
        /// </summary>
        public DateTime? FirstDate { get; private set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Ölçü farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListMeasurementDifferenceDto>? PriceListMeasurementDifferences { get; private set; }

        /// <summary>
        /// Cam deseni farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListGlassDifferenceDto>? PriceListGlassDifferences { get; private set; }

        /// <summary>
        /// Renk farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListColorDifferenceDto>? PriceListColorDifferences { get; private set; }

        /// <summary>
        /// Fiyat listesi detayları
        /// </summary>
        public List<PriceListDetailDto>? PriceListDetails { get; private set; }

        public PriceListMasterCreateDto(
            string code, string name, DateTime? firstDate = null, DateTime? endDate = null,
            List<PriceListMeasurementDifferenceDto>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifferenceDto>? priceListGlassDifferences = null,
            List<PriceListColorDifferenceDto>? priceListColorDifferences = null,
            List<PriceListDetailDto>? priceListDetails = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
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
