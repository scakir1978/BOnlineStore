namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? GlassGroupId { get; set; }

        public GlassUpdateDto(string? code, string? name, string? glassGroupId)
        {
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;
        }
    }
}
