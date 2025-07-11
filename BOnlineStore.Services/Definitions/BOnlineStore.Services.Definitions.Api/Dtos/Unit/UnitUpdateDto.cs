namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class UnitUpdateDto
    {
        /// <summary>
        /// Birim kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Birim adı
        /// </summary>
        public string? Name { get; set; }

        public UnitUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
