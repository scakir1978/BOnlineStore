using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    public class GlassDto : EntityDto
    {
        /// <summary>
        /// Cam kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cam adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cam grup kimliği
        /// </summary>
        public string GlassGroupId { get; set; }

        public GlassDto(string id, string code, string name, string glassGroupId)
        {
            Id = id;
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;
        }
    }
}
