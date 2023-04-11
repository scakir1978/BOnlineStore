namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class LengthUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public LengthUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
