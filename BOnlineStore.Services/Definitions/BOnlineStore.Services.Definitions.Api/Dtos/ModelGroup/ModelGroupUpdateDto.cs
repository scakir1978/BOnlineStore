namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupUpdateDto
    {
        /// <summary>
        /// Model grubu kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Model grubu adı
        /// </summary>
        public string? Name { get; set; }

        public ModelGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
