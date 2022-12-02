namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public ColorUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
