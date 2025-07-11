using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassCreateDto : EntityDto
    {
        /// <summary>
        /// Cam kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cam adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cam grup kimliği
        /// </summary>
        public string GlassGroupId { get; set; }

        public GlassCreateDto(string code, string name, string glassGroupId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;

        }
    }
}
