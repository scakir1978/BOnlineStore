using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
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
