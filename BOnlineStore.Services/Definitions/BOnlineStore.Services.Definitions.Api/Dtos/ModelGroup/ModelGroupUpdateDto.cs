namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public ModelGroupUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
