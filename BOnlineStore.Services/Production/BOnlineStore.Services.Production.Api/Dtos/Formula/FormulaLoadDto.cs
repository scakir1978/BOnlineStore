using BOnlineStore.Shared;
using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    public class FormulaLoadDto : EntityDto
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
    }
}
