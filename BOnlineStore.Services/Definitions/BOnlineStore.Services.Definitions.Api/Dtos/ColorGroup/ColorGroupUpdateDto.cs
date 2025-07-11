namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorGroupUpdateDto
    {
        /// <summary>
        /// Renk grubu kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Renk grubu adı
        /// </summary>
        public string? Name { get; set; }

        public ColorGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
