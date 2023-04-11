using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RawMaterialGroupCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public RawMaterialGroupCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
