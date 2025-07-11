namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class DefinitionsRequestDto
    {
        /// <summary>
        /// Tablo adı
        /// </summary>
        public string? EntityName { get; set; }

        /// <summary>
        /// Tablo kimliği
        /// </summary>
        public string? EntityId { get; set; }
    }
}
