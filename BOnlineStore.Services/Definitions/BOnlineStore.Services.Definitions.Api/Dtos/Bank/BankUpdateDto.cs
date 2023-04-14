namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class BankUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public BankUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
