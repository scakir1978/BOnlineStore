using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelComboDto : EntityDto
    {
        /// <summary>
        /// Model Kodu
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Model Açıklaması
        /// </summary>
        public string Name { get; private  set; }

        public ModelComboDto(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
