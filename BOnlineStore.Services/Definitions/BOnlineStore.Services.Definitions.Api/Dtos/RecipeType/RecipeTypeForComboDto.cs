using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RecipeTypeForComboDto : EntityDto
    {
        /// <summary>
        /// Reçete türü kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Reçete türü adı
        /// </summary>
        public string Name { get; set; }

        public RecipeTypeForComboDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }

}
