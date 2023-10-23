using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir.
    /// </summary>
    public class FormulaDetailDto : EntityDto
    {
        /// <summary>
        /// "(",")" parantez açma, kapama, dört işlem "+,-,*,/" ve değişkenler "EN1, EN2, EN3, YUKSEKLIK, SONUCDEGISKENI" ile sayısal değer içeren (1,5,20, 150) ifadelerinden biri olabilir.
        /// </summary>
        public string? VariableType { get; set; }

        /// <summary>
        /// Eğer değişten tipi (VariableType) SONUCDEGISKENI ise, formülde başka bir formül hesabının sonucu kullanılacak demektir. O yüzden formul id bilgisi bu alana kayıt edilir.
        /// </summary>
        public string? FormulId { get; set; }

        public FormulaDetailDto()
        {
            VariableType = "";
            FormulId = null;
        }

        public FormulaDetailDto(string? variableType = null, string? formulId = null)
        {
            VariableType = variableType;
            FormulId = formulId;
        }
    }
}
