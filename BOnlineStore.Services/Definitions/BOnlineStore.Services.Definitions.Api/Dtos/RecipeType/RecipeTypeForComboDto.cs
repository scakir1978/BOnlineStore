using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RecipeTypeForComboDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public RecipeTypeForComboDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }

}
