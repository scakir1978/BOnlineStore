namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorGroupUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public ColorGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
