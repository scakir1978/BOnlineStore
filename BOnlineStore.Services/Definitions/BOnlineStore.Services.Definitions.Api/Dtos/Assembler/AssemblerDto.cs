using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montör tanımlama (Montajcı)
    /// </summary>
    public class AssemblerDto : EntityDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public AssemblerDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
