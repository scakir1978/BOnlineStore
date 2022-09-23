using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }        

        public ModelGroupCreateDto(string code, string name)
        {            
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
        }
    }
}
