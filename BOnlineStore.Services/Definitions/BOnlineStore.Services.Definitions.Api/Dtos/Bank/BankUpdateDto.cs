namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Banka güncellemek için kullanılan dto.
    /// </summary>
    public class BankUpdateDto
    {
        /// <summary>
        /// Banka kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Banka adı.
        /// </summary>
        public string? Name { get; set; }

        public BankUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
