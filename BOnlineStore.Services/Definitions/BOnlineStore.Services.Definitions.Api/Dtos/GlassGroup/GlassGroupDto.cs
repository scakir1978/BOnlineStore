using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassGroupDto : EntityDto
    {
        /// <summary>
        /// Cam grubu kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cam grubu adı
        /// </summary>
        public string Name { get; set; }

        public GlassGroupDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
