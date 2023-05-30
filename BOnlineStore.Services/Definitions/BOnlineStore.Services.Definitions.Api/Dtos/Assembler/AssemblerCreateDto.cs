using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Montör (Montajcı) eklemek için kullanılan dto
    /// </summary>
    public class AssemblerCreateDto : EntityDto
    {
        /// <summary>
        /// Montör kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Montör adı.
        /// </summary>
        public string? Name { get; set; }

        public AssemblerCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
