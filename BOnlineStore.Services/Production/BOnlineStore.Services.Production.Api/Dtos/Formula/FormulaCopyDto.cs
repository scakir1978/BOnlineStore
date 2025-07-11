namespace BOnlineStore.Services.Production.Api.Dtos.Formula
{
    public class FormulaCopyDto
    {
        /// <summary>
        /// Formül kimliği
        /// </summary>
        public string? FormulaId { get; set; }

        /// <summary>
        /// Formül kodu
        /// </summary>
        public string? FormulaCode { get; set; }

        /// <summary>
        /// Model kimliği
        /// </summary>
        public string? ModelId { get; set; }

        /// <summary>
        /// Panel kimliği
        /// </summary>
        public string? PanelId { get; set; }
    }
}
