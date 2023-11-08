namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Mahalle güncellemek için kullanılan dto
    /// </summary>
    public class DistrictUpdateDto
    {
        /// <summary>
        /// Mahalle kodu.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Mahalle adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// İlçe id
        /// </summary>
        public string? CountyId { get; set; }

        public DistrictUpdateDto(string? code, string? name, string? countyId = null)
        {
            Code = code;
            Name = name;
            CountyId = countyId;
        }
    }
}
