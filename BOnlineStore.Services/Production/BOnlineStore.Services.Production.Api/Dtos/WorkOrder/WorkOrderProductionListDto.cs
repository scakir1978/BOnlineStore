using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// İmalat formunda fomüle göre yapılan hesapları döndüren dto
    /// </summary>
    public class WorkOrderProductionListDto : EntityDto
    {
        /// <summary>
        /// İş emri Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? WorkOrderId { get; set; }

        /// <summary>
        /// Formül Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FormulId { get; set; }
        /// <summary>
        /// Formül Adı
        /// </summary>
        public string? FormulName { get; set; }
        /// <summary>
        /// Formülde Kullanılan Malzeme Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RawMaterialId { get; set; }

        /// <summary>
        /// Formülde kullanılan malzeme panele ait ise true olur.
        /// </summary>
        public bool IsPanel { get; set; }

        /// <summary>
        /// Üretim Adedi
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Üretim Ölçsü
        /// </summary>
        public decimal? ProductionMeasure { get; set; }

    }
}
