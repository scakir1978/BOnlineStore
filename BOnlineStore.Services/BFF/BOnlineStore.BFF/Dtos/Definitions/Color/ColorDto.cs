using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    public class ColorDto : EntityDto
    {
        /// <summary>
        /// Renk kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Renk adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Renk grup kimliği
        /// </summary>
        public string ColorGroupId { get; set; }

        public ColorDto(string id, string code, string name, string colorGroupId)
        {
            Id = id;
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }
    }
}
