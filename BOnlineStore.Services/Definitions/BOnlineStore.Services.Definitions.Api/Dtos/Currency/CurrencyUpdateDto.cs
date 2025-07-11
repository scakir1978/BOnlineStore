namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class CurrencyUpdateDto
    {
        /// <summary>
        /// Para birimi kodu (örn: USD, EUR, TRY)
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Para birimi adı (örn: Amerikan Doları, Euro, Türk Lirası)
        /// </summary>
        public string? Name { get; set; }

        public CurrencyUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
