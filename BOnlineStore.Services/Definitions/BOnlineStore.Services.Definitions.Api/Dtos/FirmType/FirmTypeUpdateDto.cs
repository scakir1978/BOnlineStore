namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class FirmTypeUpdateDto
    {
        /// <summary>
        /// Firma türü kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Firma türü adı
        /// </summary>
        public string? Name { get; set; }

        public FirmTypeUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
