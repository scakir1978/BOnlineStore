using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class FirmForComboDto : EntityDto
    {
        /// <summary>
        /// Firma Kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Firma Adı
        /// </summary>
        public string Name { get; set; }

        public FirmForComboDto(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
