namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montör tanımlama (Montajcı)
    /// </summary>
    public class AssemblerUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public AssemblerUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
