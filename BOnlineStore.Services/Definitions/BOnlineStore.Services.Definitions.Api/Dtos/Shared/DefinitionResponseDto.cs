namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class DefinitionResponseDto
    {
        /// <summary>
        /// Tablo adı
        /// </summary>
        public string? EntityName { get; set; }

        /// <summary>
        /// Tablo nesnesi
        /// </summary>
        public object? Entity { get; set; }
    }
}
