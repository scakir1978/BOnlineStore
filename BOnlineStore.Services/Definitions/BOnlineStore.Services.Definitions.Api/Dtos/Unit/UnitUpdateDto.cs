namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class UnitUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public UnitUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
