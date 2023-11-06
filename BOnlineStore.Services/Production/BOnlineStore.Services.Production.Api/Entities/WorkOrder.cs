using BOnlineStore.Shared;
using BOnlineStore.Shared.Entities;
using BOnlineStore.Shared.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Entities
{
    /// <summary>
    /// Üretim emri
    /// </summary>
    public class WorkOrder : Entity
    {
        /// <summary>
        /// İş emri numarası
        /// </summary>
        public string WorkOrderNo { get; private set; }

        /// <summary>
        /// İş emri açıklaması
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Üretimi yapılacak modelin idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModelId { get; private set; }

        /// <summary>
        /// Üretim adedi
        /// </summary>
        public decimal? Amount { get; private set; }

        /// <summary>
        /// En-1 ölçüsü
        /// </summary>
        public decimal? Width1 { get; private set; }

        /// <summary>
        /// En-2 ölçüsü
        /// </summary>
        public decimal? Width2 { get; private set; }

        /// <summary>
        /// En-3 ölçüsü
        /// </summary>
        public decimal? Width3 { get; private set; }

        /// <summary>
        /// Yükseklik ölçüsü
        /// </summary>
        public decimal? Height { get; private set; }

        /// <summary>
        /// Panel en ölçüsü
        /// </summary>
        public decimal? PanelWidth { get; private set; }

        /// <summary>
        /// Panel yükseklik ölçüsü
        /// </summary>
        public decimal? PanelHeight { get; private set; }

        /// <summary>
        /// Yan panel en ölçüsü
        /// </summary>
        public decimal? SidePanelWidth { get; private set; }

        /// <summary>
        /// Yan panel yükseklik ölçüsü
        /// </summary>
        public decimal? SidePanelHeight { get; private set; }

        /// <summary>
        /// Cam türü idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GlassId { get; private set; }

        /// <summary>
        /// Renk idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ColorId { get; private set; }

        /// <summary>
        /// Havluluk bilgisi
        /// </summary>
        public string? TowelBar { get; private set; }

        /// <summary>
        /// Açılış yönü
        /// </summary>
        public SwingDirectionEnum.SwingDirection? SwingDirection { get; private set; }

        /// <summary>
        /// İş emri durumu
        /// </summary>
        public WorkOrderStatusEnum.WorkOrderStatus? Status { get; private set; }

        /// <summary>
        /// Sipariş tarihi
        /// </summary>
        public DateTime? OrderDate { get; private set; }

        /// <summary>
        /// Üretim tarihi
        /// </summary>
        public DateTime? ProductionDate { get; private set; }

        /// <summary>
        /// Atöyle çıkış tarihi
        /// </summary>
        public DateTime? WorkshopReleaseDate { get; private set; }

        /// <summary>
        /// Teslim tarihi
        /// </summary>
        public DateTime? DeliveryDate { get; private set; }

        /// <summary>
        /// Firma idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FirmId { get; private set; }

        /// <summary>
        /// Sevk adresi idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryAddressId { get; private set; }

        /// <summary>
        /// Şablon idsi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TemplateId { get; private set; }

        /// <summary>
        /// Takoz bilgisi
        /// </summary>
        public string? Shim { get; private set; }

        /// <summary>
        /// Montaj poşeti bilgisi
        /// </summary>
        public string? AssemblyKit { get; private set; }

        /// <summary>
        /// Kontol elemanı
        /// </summary>
        public string? QualityControlInspector { get; private set; }

        /// <summary>
        /// Teslim alan
        /// </summary>
        public string? Consignee { get; private set; }

        public WorkOrder() : base()
        {
            WorkOrderNo = "";
            Description = "";
        }

        public WorkOrder(Guid tenantId, string id, string workOrderNo, string description, string? modelId = null, decimal? amount = null,
            decimal? width1 = null, decimal? width2 = null, decimal? width3 = null, decimal? height = null,
            decimal? panelWidth = null, decimal? panelHeight = null, decimal? sidePanelWidth = null,
            decimal? sidePanelHeight = null, string? glassId = null, string? colorId = null, string? towelBar = null,
            SwingDirectionEnum.SwingDirection? swingDirection = null, WorkOrderStatusEnum.WorkOrderStatus? status = null,
            DateTime? orderDate = null, DateTime? productionDate = null, DateTime? workshopReleaseDate = null,
            DateTime? deliveryDate = null, string? firmId = null, string? deliveryAddressId = null,
            string? templateId = null, string? shim = null, string? assemblyKit = null,
            string? qualityControlInspector = null, string? consignee = null) : base(tenantId, id)
        {
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
            DeliveryAddressId = deliveryAddressId;
            TemplateId = templateId;
            Shim = shim;
            AssemblyKit = assemblyKit;
            QualityControlInspector = qualityControlInspector;
            Consignee = consignee;
        }

        public void UpdateWorkOrder(string workOrderNo, string description)
        {
            WorkOrderNo = workOrderNo;
            Description = description;
        }
    }
}
