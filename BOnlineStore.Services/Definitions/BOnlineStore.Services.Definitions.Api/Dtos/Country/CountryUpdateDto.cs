namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// Ülke güncellemek için kullanılan dto
    public class CountryUpdateDto
    {
        /// <summary>
        /// Ülke kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Ülke adı.
        /// </summary>
        public string? Name { get; set; }

        public CountryUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
