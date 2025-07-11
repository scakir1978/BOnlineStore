namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class TemplateUpdateDto
    {
        /// <summary>
        /// Şablon kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Şablon adı
        /// </summary>
        public string? Name { get; set; }

        public TemplateUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
