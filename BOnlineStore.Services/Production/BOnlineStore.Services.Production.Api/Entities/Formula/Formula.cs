using BOnlineStore.Shared;
using BOnlineStore.Shared.Entities;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Entities
{
    /// <summary>
    /// Formül tanımları
    /// </summary>
    public class Formula : Entity, IAggregateRoot
    {
        /// <summary>
        /// Formül kodu
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Formül Adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Model
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModelId { get; private set; }

        /// <summary>
        /// Hammadde
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RawMaterialId { get; private set; }

        /// <summary>
        /// Formül tipi
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FormulaTypeId { get; private set; }

        /// <summary>
        /// Kullanım miktarı
        /// </summary>
        public decimal? UsageAmount { get; private set; }

        /// <summary>
        /// Matematiksel olarak oluşan formül
        /// </summary>
        public string? FormulaText { get; private set; }

        /// <summary>
        /// Formül türü
        /// </summary>
        public FormulaSortEnum.FormulaSort? FormulaSort { get; private set; }

        /// <summary>
        /// Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir.
        /// </summary>
        public List<FormulaDetail>? FormulaDetails { get; private set; }


        public Formula() : base()
        {
            Code = "";
            Name = "";
        }

        public Formula(Guid tenantId, string id, string code, string name, string? modelId = null,
                       string? rawMaterialId = null, string? formulaTypeId = null, decimal? usageAmount = null,
                       FormulaSortEnum.FormulaSort? formulaSort = null, string? formulaText = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            ModelId = modelId;
            RawMaterialId = rawMaterialId;
            FormulaTypeId = formulaTypeId;
            UsageAmount = usageAmount;
            FormulaSort = formulaSort;
            FormulaText = formulaText;
        }

        public void UpdateFormula(string code, string name, string? modelId = null,
                       string? rawMaterialId = null, string? formulaTypeId = null, decimal? usageAmount = null,
                       FormulaSortEnum.FormulaSort? formulaSort = null, string? formulaText = null)
        {
            Code = code;
            Name = name;
            ModelId = modelId;
            RawMaterialId = rawMaterialId;
            FormulaTypeId = formulaTypeId;
            UsageAmount = usageAmount;
            FormulaSort = formulaSort;
            FormulaText = formulaText;
        }
    }
}
