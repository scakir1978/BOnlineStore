using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorGroupId { get; set; }

        public ColorDto(string id, string code, string name, string colorGroupId)
        {
            Id = id;
            Code = code;
            Name = name;
            ColorGroupId= colorGroupId;
        }
    }
}
