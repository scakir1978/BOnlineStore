namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassGroupUpdateDto
    {
        /// <summary>
        /// Cam grubu kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Cam grubu adı
        /// </summary>
        public string? Name { get; set; }

        public GlassGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
