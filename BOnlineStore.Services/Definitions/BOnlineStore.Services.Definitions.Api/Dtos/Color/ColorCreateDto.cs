using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorGroupId { get; set; }

        public ColorCreateDto(string code, string name, string colorGroupId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }
    }
}
