using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.BFF.Api.Dtos
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
            ColorGroupId = colorGroupId;
        }
    }
}
