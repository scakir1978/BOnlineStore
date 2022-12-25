using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ModelGroupId { get; set; }

        public ModelUpdateDto(string? code, string? name, string? modelGroupId)
        {
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }
    }
}
