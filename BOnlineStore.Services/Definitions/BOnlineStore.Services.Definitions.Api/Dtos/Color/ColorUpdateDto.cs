using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ColorGroupId { get; set; }

        public ColorUpdateDto(string? code, string? name, string? colorGroupId)
        {
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }
    }
}
