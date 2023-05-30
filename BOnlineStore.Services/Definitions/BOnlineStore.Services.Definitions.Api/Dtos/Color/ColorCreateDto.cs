using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Renk
    /// </summary>
    public class ColorCreateDto : EntityDto
    {
        /// <summary>
        /// Renk kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Renk adı.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Renk grup adı.
        /// </summary>
        public string? ColorGroupId { get; set; }

        public ColorCreateDto(string code, string name, string colorGroupId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }
    }
}
