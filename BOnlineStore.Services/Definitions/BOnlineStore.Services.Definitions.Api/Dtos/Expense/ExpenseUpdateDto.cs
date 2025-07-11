namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ExpenseUpdateDto
    {
        /// <summary>
        /// Gider kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gider adı
        /// </summary>
        public string? Name { get; set; }

        public ExpenseUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
