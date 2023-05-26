namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class DistrictUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public DistrictUpdateDto(string? code, string? name)
        {
            Code = code;
            Name = name;
        }
    }
}
