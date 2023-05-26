using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montör tanımlama
    /// </summary>
    public class AssemblerCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public AssemblerCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
