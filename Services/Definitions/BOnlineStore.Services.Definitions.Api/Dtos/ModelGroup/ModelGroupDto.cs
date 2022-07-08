using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public ModelGroupDto(Guid id, string code, string name)
        {            
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
