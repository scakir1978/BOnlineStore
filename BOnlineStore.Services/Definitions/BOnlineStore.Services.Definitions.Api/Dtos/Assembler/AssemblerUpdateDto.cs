namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montör (Montajcı) güncellemek için kullanılan dto
    /// </summary>
    public class AssemblerUpdateDto
    {
        /// <summary>
        /// Montör kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Montör adı
        /// </summary>
        public string? Name { get; set; }

        public AssemblerUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
