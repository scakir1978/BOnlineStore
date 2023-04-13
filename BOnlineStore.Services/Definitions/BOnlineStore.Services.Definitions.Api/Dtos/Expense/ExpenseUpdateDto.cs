namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ExpenseUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public ExpenseUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
