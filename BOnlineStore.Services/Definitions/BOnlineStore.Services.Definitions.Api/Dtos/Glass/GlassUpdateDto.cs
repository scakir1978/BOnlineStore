namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassUpdateDto
    {
        /// <summary>
        /// Cam kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Cam adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Cam grup kimliği
        /// </summary>
        public string? GlassGroupId { get; set; }

        public GlassUpdateDto(string? code, string? name, string? glassGroupId)
        {
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;
        }
    }
}
