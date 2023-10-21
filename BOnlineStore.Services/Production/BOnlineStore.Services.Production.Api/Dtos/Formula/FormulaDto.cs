using BOnlineStore.Shared;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// Formül tanımlama
    /// </summary>
    public class FormulaDto : EntityDto
    {
        /// <summary>
        ///Formül kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        ///Formül adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        public string? ModelId { get; private set; }

        /// <summary>
        /// Hammadde
        /// </summary>
        public string? RawMaterialId { get; private set; }

        /// <summary>
        /// Formül tipi
        /// </summary>
        public string? FormulaTypeId { get; private set; }

        /// <summary>
        /// Kullanım miktarı
        /// </summary>
        public decimal? UsageAmount { get; private set; }

        /// <summary>
        /// Değişken-1
        /// </summary>
        public string? Variable1 { get; private set; }

        /// <summary>
        /// Değişken-2
        /// </summary>
        public string? Variable2 { get; private set; }

        /// <summary>
        /// Değişken-3
        /// </summary>
        public string? Variable3 { get; private set; }

        /// <summary>
        /// Değişken-4
        /// </summary>
        public string? Variable4 { get; private set; }

        /// <summary>
        /// Sabit-1
        /// </summary>
        public decimal? Constant1 { get; private set; }

        /// <summary>
        /// Sabit-2
        /// </summary>
        public decimal? Constant2 { get; private set; }

        /// <summary>
        /// Formül türü
        /// </summary>
        public FormulaSortEnum.FormulaSort? FormulaSort { get; private set; }

        public FormulaDto(string id, string code, string name, string? modelId = null, string? rawMaterialId = null,
                          string? formulaTypeId = null, decimal? usageAmount = null, string? variable1 = null,
                          string? variable2 = null, string? variable3 = null, string? variable4 = null,
                          decimal? constant1 = null, decimal? constant2 = null,
                          FormulaSortEnum.FormulaSort? formulaSort = null)
        {
            Id = id;
            Code = code;
            Name = name;
            ModelId = modelId;
            RawMaterialId = rawMaterialId;
            FormulaTypeId = formulaTypeId;
            UsageAmount = usageAmount;
            Variable1 = variable1;
            Variable2 = variable2;
            Variable3 = variable3;
            Variable4 = variable4;
            Constant1 = constant1;
            Constant2 = constant2;
            FormulaSort = formulaSort;
        }
    }
}
