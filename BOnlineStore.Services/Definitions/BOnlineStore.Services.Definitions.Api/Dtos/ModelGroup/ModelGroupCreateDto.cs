using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }        

        public ModelGroupCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
