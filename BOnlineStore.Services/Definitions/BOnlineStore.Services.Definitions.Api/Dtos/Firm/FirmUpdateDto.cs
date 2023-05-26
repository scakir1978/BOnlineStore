namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class FirmUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public FirmUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
