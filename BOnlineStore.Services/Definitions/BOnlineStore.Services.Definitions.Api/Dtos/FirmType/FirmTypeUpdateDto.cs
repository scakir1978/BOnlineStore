namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class FirmTypeUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public FirmTypeUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
