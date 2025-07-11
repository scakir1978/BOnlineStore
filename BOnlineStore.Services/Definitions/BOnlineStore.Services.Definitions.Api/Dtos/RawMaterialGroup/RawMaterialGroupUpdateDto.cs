namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RawMaterialGroupUpdateDto
    {
        /// <summary>
        /// Hammadde grubu kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Hammadde grubu adı
        /// </summary>
        public string? Name { get; set; }

        public RawMaterialGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
