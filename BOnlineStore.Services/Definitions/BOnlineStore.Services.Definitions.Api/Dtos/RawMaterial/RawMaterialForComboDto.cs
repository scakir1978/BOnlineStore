using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RawMaterialForComboDto : EntityDto
    {
        /// <summary>
        /// Hammadde Kodu
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Hammadde Adı
        /// </summary>
        public string Name { get; set; }

        public RawMaterialForComboDto(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
