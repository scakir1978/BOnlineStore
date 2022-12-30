namespace BOnlineStore.Services.Production.Api.Dtos
{
    public class FormulaTypeUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public FormulaTypeUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
