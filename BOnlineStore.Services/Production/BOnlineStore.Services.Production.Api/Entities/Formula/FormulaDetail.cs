using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Production.Api.Entities
{
    /// <summary>
    /// Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir.
    /// </summary>
    public class FormulaDetail : IdEntity, IAggregateRoot
    {
        /// <summary>
        /// "(",")" parantez açma, kapama, dört işlem "+,-,*,/" ve değişkenler "EN1, EN2, EN3, YUKSEKLIK, SONUCDEGISKENI" ile sayısal değer içeren (1,5,20, 150) ifadelerinden biri olabilir.
        /// </summary>
        public string? VariableType { get; private set; }

        /// <summary>
        /// Eğer değişten tipi (VariableType) SONUCDEGISKENI ise, formülde başka bir formül hesabının sonucu kullanılacak demektir. O yüzden formul id bilgisi bu alana kayıt edilir.
        /// </summary>
        public string? FormulId { get; private set; }

        /// <summary>
        /// Eğer değişten tipi (VariableType) SABIT ise, Bu alana sayısal bir bilgi girilir..
        /// </summary>
        public decimal? VariableValue { get; private set; }

        public FormulaDetail()
        {
            VariableType = "";
            VariableValue = 0;
            FormulId = null;

        }

        public FormulaDetail(string? variableType = null, string? formulId = null, decimal? variableValue = null)
        {
            VariableType = variableType;
            FormulId = formulId;
            VariableValue = variableValue;
        }
    }
}
