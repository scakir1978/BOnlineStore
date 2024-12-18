using BOnlineStore.Shared;
using BOnlineStore.Shared.Entity;
using BOnlineStore.Shared.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// Üretim emri
    /// </summary>
    public class WorkOrderDto : EntityDto
    {
        /// <summary>
        /// İş emri numarası
        /// </summary>
        public string? WorkOrderNo { get; set; }

        /// <summary>
        /// İş emri açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Üretimi yapılacak modelin idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModelId { get; set; }

        /// <summary>
        /// Üretim adedi
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// En-1 ölçüsü
        /// </summary>
        public decimal? Width1 { get; set; }

        /// <summary>
        /// En-2 ölçüsü
        /// </summary>
        public decimal? Width2 { get; set; }

        /// <summary>
        /// En-3 ölçüsü
        /// </summary>
        public decimal? Width3 { get; set; }

        /// <summary>
        /// Yükseklik ölçüsü
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// Panel en ölçüsü
        /// </summary>
        public decimal? PanelWidth { get; set; }

        /// <summary>
        /// Panel yükseklik ölçüsü
        /// </summary>
        public decimal? PanelHeight { get; set; }

        /// <summary>
        /// Yan panel en ölçüsü
        /// </summary>
        public decimal? SidePanelWidth { get; set; }

        /// <summary>
        /// Yan panel yükseklik ölçüsü
        /// </summary>
        public decimal? SidePanelHeight { get; set; }

        /// <summary>
        /// Cam türü idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GlassId { get; set; }

        /// <summary>
        /// Renk idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ColorId { get; set; }

        /// <summary>
        /// Havluluk bilgisi
        /// </summary>
        public string? TowelBar { get; set; }

        /// <summary>
        /// Açılış yönü
        /// </summary>
        public SwingDirectionEnum.SwingDirection? SwingDirection { get; set; }

        /// <summary>
        /// İş emri durumu
        /// </summary>
        public WorkOrderStatusEnum.WorkOrderStatus? Status { get; set; }

        /// <summary>
        /// Sipariş tarihi
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Üretim tarihi
        /// </summary>
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// Atöyle çıkış tarihi
        /// </summary>
        public DateTime? WorkshopReleaseDate { get; set; }

        /// <summary>
        /// Teslim tarihi
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Firma idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FirmId { get; set; }

        /// <summary>
        /// Sevk adresi idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryAdressId { get; set; }

        /// <summary>
        /// Şablon idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TemplateId { get; set; }

        /// <summary>
        /// Takoz bilgisi
        /// </summary>
        public string? Shim { get; set; }

        /// <summary>
        /// Montaj poşeti bilgisi
        /// </summary>
        public string? AssemblyKit { get; set; }

        /// <summary>
        /// Kontol elemanı
        /// </summary>
        public string? QualityControlInspector { get; set; }

        /// <summary>
        /// Teslim alan
        /// </summary>
        public string? Consignee { get; set; }

        public WorkOrderDto(string id, string? workOrderNo = null, string? description = null, string? modelId = null,
            decimal? amount = null, decimal? width1 = null, decimal? width2 = null, decimal? width3 = null,
            decimal? height = null, decimal? panelWidth = null, decimal? panelHeight = null,
            decimal? sidePanelWidth = null, decimal? sidePanelHeight = null, string? glassId = null,
            string? colorId = null, string? towelBar = null, SwingDirectionEnum.SwingDirection? swingDirection = null,
            WorkOrderStatusEnum.WorkOrderStatus? status = null, DateTime? orderDate = null,
            DateTime? productionDate = null, DateTime? workshopReleaseDate = null, DateTime? deliveryDate = null,
            string? firmId = null, string? deliveryAddressId = null, string? templateId = null, string? shim = null,
            string? assemblyKit = null, string? qualityControlInspector = null, string? consignee = null)
        {
            Id = id;
            WorkOrderNo = workOrderNo;
            Description = description;
            ModelId = modelId;
            Amount = amount;
            Width1 = width1;
            Width2 = width2;
            Width3 = width3;
            Height = height;
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            SidePanelWidth = sidePanelWidth;
            SidePanelHeight = sidePanelHeight;
            GlassId = glassId;
            ColorId = colorId;
            TowelBar = towelBar;
            SwingDirection = swingDirection;
            Status = status;
            OrderDate = orderDate;
            ProductionDate = productionDate;
            WorkshopReleaseDate = workshopReleaseDate;
            DeliveryDate = deliveryDate;
            FirmId = firmId;
            DeliveryAdressId = deliveryAddressId;
            TemplateId = templateId;
            Shim = shim;
            AssemblyKit = assemblyKit;
            QualityControlInspector = qualityControlInspector;
            Consignee = consignee;
        }
    }
}
