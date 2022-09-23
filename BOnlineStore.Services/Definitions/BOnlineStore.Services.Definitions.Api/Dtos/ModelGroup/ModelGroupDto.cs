using BOnlineStore.Shared.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupDto : EntityDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public ModelGroupDto(Guid id, string code, string name)
        {            
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
