namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class TemplateUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public TemplateUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
