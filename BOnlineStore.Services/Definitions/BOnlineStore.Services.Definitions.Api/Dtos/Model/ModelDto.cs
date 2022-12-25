using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ModelGroupId { get; set; }

        public ModelDto(string id, string code, string name, string modelGroupId)
        {
            Id = id;
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }
    }
}
