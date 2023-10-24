using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelForComboDto : EntityDto
    {
        /// <summary>
        /// Model Kodu
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Model Açıklaması
        /// </summary>
        public string Name { get; private set; }

        public ModelForComboDto(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
