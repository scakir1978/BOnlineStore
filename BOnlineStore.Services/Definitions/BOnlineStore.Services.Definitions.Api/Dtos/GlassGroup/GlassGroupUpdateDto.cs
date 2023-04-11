namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassGroupUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public GlassGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
