using BOnlineStore.Shared.Entities;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Fiyat listesi master entity
    /// </summary>
    public class PriceListMaster : Entity, IAggregateRoot
    {
        /// <summary>
        /// Fiyat listesi kodu.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Fiyatlistesi açıklaması veya adı
        /// </summary>
        public string Name { get; private set; }

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
        public List<PriceListMeasurementDifference>? PriceListMeasurementDifferences { get; private set; }

        /// <summary>
        /// Cam deseni farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListGlassDifference>? PriceListGlassDifferences { get; private set; }

        /// <summary>
        /// Renk farklarından oluşan fiyat bilgileri
        /// </summary>
        public List<PriceListColorDifference>? PriceListColorDifferences { get; private set; }

        /// <summary>
        /// Fiyat listesi detayları
        /// </summary>
        public List<PriceListDetail>? PriceListDetails { get; private set; }

        public PriceListMaster() : base()
        {
            Code = "";
            Name = "";
        }

        public PriceListMaster(
            Guid tenantId, string id, string code, string name, DateTime? firstDate = null, DateTime? endDate = null,
            List<PriceListMeasurementDifference>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifference>? priceListGlassDifferences = null,
            List<PriceListColorDifference>? priceListColorDifferences = null, List<PriceListDetail>? priceListDetails = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            FirstDate = firstDate;
            EndDate = endDate;
            PriceListMeasurementDifferences = priceListMeasurementDifferences;
            PriceListGlassDifferences = priceListGlassDifferences;
            PriceListColorDifferences = priceListColorDifferences;
            PriceListDetails = priceListDetails;
        }

        public void UpdatePriceListMaster(string code, string name, DateTime? firstDate = null, DateTime? endDate = null,
            List<PriceListMeasurementDifference>? priceListMeasurementDifferences = null,
            List<PriceListGlassDifference>? priceListGlassDifferences = null,
            List<PriceListColorDifference>? priceListColorDifferences = null,
            List<PriceListDetail>? priceListDetails = null)
        {
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
