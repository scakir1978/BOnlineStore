﻿using BOnlineStore.Shared;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// Formül güncellemek için kullanılan dto
    /// </summary>
    public class FormulaUpdateDto
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
        public string? ModelId { get; set; }

        /// <summary>
        /// Panel
        /// </summary>
        public string? PanelId { get; set; }

        /// <summary>
        /// Hammadde
        /// </summary>
        public string? RawMaterialId { get; set; }

        /// <summary>
        /// Formül tipi
        /// </summary>
        public string? FormulaTypeId { get; set; }

        /// <summary>
        /// Kullanım miktarı
        /// </summary>
        public decimal? UsageAmount { get; set; }

        /// <summary>
        /// Matematiksel olarak oluşan formül
        /// </summary>
        public string? FormulaText { get; set; }

        /// <summary>
        /// Formül türü
        /// </summary>
        public FormulaSortEnum.FormulaSort? FormulaSort { get; set; }

        /// <summary>
        /// Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir.
        /// </summary>
        public List<FormulaDetailDto>? FormulaDetails { get; set; }

        public FormulaUpdateDto(string? code, string? name, string? modelId = null, string? rawMaterialId = null,
                                string? formulaTypeId = null, decimal? usageAmount = null,
                                FormulaSortEnum.FormulaSort? formulaSort = null, string? formulaText = null, List<FormulaDetailDto>? formulaDetails = null)
        {
            Code = code;
            Name = name;
            ModelId = modelId;
            RawMaterialId = rawMaterialId;
            FormulaTypeId = formulaTypeId;
            UsageAmount = usageAmount;
            FormulaSort = formulaSort;
            FormulaText = formulaText;
            FormulaDetails = formulaDetails;
        }
    }
}
