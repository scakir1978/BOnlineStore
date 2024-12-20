using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class PanelForComboDto : EntityDto
    {
        /// <summary>
        /// Panel Kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Panel Açıklaması
        /// </summary>
        public string Name { get; set; }

        public PanelForComboDto(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
