using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ModelGroupId { get; set; }

        public ModelCreateDto(string code, string name, string modelGroupId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }
    }
}
