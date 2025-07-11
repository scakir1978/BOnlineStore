namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class LengthUpdateDto
    {
        /// <summary>
        /// Boy kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Boy adı
        /// </summary>
        public string? Name { get; set; }

        public LengthUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
