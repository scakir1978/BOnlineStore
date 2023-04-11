namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RawMaterialGroupUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public RawMaterialGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
