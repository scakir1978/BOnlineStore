namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class CurrencyUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public CurrencyUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
