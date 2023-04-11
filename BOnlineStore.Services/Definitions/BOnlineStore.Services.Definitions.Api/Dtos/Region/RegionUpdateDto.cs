namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RegionUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public RegionUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
