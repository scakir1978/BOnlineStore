using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api
{
    /// <summary>
    /// Hammadde
    /// </summary>
    public class RawMaterial : Entity
    {
        /// <summary>
        /// Hammadde Kodu
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Hammadde Adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Hammaddenin ana grubu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BaseGroupId { get; private set; }
        /// <summary>
        /// Hammaddenin grubu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GroupId { get; private set; }
        /// <summary>
        /// Kritik miktar
        /// </summary>
        public decimal? CriticalAmount { get; private set; }
        /// <summary>
        /// Devir tarihi
        /// </summary>
        public DateTime? TransferDate { get; private set; }
        /// <summary>
        /// Kdv oranı
        /// </summary>
        public decimal? VatRatio { get; private set; }
        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public decimal? SalePrice01 { get; private set; }
        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public decimal? SalePrice02 { get; private set; }
        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public decimal? SalePrice03 { get; private set; }
        /// <summary>
        /// Stok alt detayı
        /// </summary>
        public int? StockSubDetail { get; private set; }
        /// <summary>
        /// Alış fiyatı
        /// </summary>
        public decimal? PurchasePrice01 { get; private set; }
        /// <summary>
        /// Alış fiyatı
        /// </summary>
        public decimal? PurchasePrice02 { get; private set; }
        /// <summary>
        /// Stok birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StockUnit01Id { get; private set; }
        /// <summary>
        /// Stok birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StockUnit02Id { get; private set; }
        /// <summary>
        /// Stok birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StockUnit03Id { get; private set; }
        /// <summary>
        /// Stok birim dönüşüm oranı
        /// </summary>
        public decimal? StockUnitTransformationRatio01 { get; private set; }
        /// <summary>
        /// Stok birim dönüşüm oranı
        /// </summary>
        public decimal? StockUnitTransformationRatio02 { get; private set; }
        /// <summary>
        /// Stok birim dönüşüm oranı
        /// </summary>
        public decimal? StockUnitTransformationRatio03 { get; private set; }
        /// <summary>
        /// Üretim birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductUnit01Id { get; private set; }
        /// <summary>
        /// Üretim birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductUnit02Id { get; private set; }
        /// <summary>
        /// Üretim birimi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductUnit03Id { get; private set; }
        /// <summary>
        /// Üretim birim dönüşüm oranı
        /// </summary>
        public decimal? ProductUnitTransformationRatio01 { get; private set; }
        /// <summary>
        /// Üretim birim dönüşüm oranı
        /// </summary>
        public decimal? ProductUnitTransformationRatio02 { get; private set; }
        /// <summary>
        /// SÜretim birim dönüşüm oranı
        /// </summary>
        public decimal? ProductUnitTransformationRatio03 { get; private set; }

        public RawMaterial() : base()
        {
            Code = "";
            Name = "";
        }

        public RawMaterial(
            Guid tenantId, string id, string code, string name, string? baseGroupId = null, string? groupId = null,
            decimal? criticalAmount = null, DateTime? transferDate = null, decimal? vatRatio = null,
            decimal? salePrice01 = null, decimal? salePrice02 = null, decimal? salePrice03 = null,
            int? stockSubDetail = null, decimal? purchasePrice01 = null, decimal? purchasePrice02 = null,
            string? stockUnit01Id = null, string? stockUnit02Id = null, string? stockUnit03Id = null,
            decimal? stockUnitTransformationRatio01 = null, decimal? stockUnitTransformationRatio02 = null,
            decimal? stockUnitTransformationRatio03 = null, string? productUnit01Id = null,
            string? productUnit02Id = null, string? productUnit03Id = null,
            decimal? productUnitTransformationRatio01 = null, decimal? productUnitTransformationRatio02 = null,
            decimal? productUnitTransformationRatio03 = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            BaseGroupId = baseGroupId;
            GroupId = groupId;
            CriticalAmount = criticalAmount;
            TransferDate = transferDate;
            VatRatio = vatRatio;
            SalePrice01 = salePrice01;
            SalePrice02 = salePrice02;
            SalePrice03 = salePrice03;
            StockSubDetail = stockSubDetail;
            PurchasePrice01 = purchasePrice01;
            PurchasePrice02 = purchasePrice02;
            StockUnit01Id = stockUnit01Id;
            StockUnit02Id = stockUnit02Id;
            StockUnit03Id = stockUnit03Id;
            StockUnitTransformationRatio01 = stockUnitTransformationRatio01;
            StockUnitTransformationRatio02 = stockUnitTransformationRatio02;
            StockUnitTransformationRatio03 = stockUnitTransformationRatio03;
            ProductUnit01Id = productUnit01Id;
            ProductUnit02Id = productUnit02Id;
            ProductUnit03Id = productUnit03Id;
            ProductUnitTransformationRatio01 = productUnitTransformationRatio01;
            ProductUnitTransformationRatio02 = productUnitTransformationRatio02;
            ProductUnitTransformationRatio03 = productUnitTransformationRatio03;
        }

        public void UpdateRawMaterial(string code, string name, string? baseGroupId = null, string? groupId = null,
            decimal? criticalAmount = null, DateTime? transferDate = null, decimal? vatRatio = null,
            decimal? salePrice01 = null, decimal? salePrice02 = null, decimal? salePrice03 = null,
            int? stockSubDetail = null, decimal? purchasePrice01 = null, decimal? purchasePrice02 = null,
            string? stockUnit01Id = null, string? stockUnit02Id = null, string? stockUnit03Id = null,
            decimal? stockUnitTransformationRatio01 = null, decimal? stockUnitTransformationRatio02 = null,
            decimal? stockUnitTransformationRatio03 = null, string? productUnit01Id = null,
            string? productUnit02Id = null, string? productUnit03Id = null,
            decimal? productUnitTransformationRatio01 = null, decimal? productUnitTransformationRatio02 = null,
            decimal? productUnitTransformationRatio03 = null)
        {
            Code = code;
            Name = name;
            BaseGroupId = baseGroupId;
            GroupId = groupId;
            CriticalAmount = criticalAmount;
            TransferDate = transferDate;
            VatRatio = vatRatio;
            SalePrice01 = salePrice01;
            SalePrice02 = salePrice02;
            SalePrice03 = salePrice03;
            StockSubDetail = stockSubDetail;
            PurchasePrice01 = purchasePrice01;
            PurchasePrice02 = purchasePrice02;
            StockUnit01Id = stockUnit01Id;
            StockUnit02Id = stockUnit02Id;
            StockUnit03Id = stockUnit03Id;
            StockUnitTransformationRatio01 = stockUnitTransformationRatio01;
            StockUnitTransformationRatio02 = stockUnitTransformationRatio02;
            StockUnitTransformationRatio03 = stockUnitTransformationRatio03;
            ProductUnit01Id = productUnit01Id;
            ProductUnit02Id = productUnit02Id;
            ProductUnit03Id = productUnit03Id;
            ProductUnitTransformationRatio01 = productUnitTransformationRatio01;
            ProductUnitTransformationRatio02 = productUnitTransformationRatio02;
            ProductUnitTransformationRatio03 = productUnitTransformationRatio03;
        }
    }
}
