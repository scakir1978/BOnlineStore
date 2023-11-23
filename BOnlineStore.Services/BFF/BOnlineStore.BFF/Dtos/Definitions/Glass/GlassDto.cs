using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    public class GlassDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
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
