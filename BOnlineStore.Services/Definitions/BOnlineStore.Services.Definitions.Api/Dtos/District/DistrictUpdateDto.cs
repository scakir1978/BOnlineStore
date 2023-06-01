namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// İlçe
    /// </summary>
    public class DistrictUpdateDto
    {
        /// <summary>
        /// İlçe kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// İlçe adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Şehir Id
        /// </summary>        
        public string? CityId { get; private set; }

        public DistrictUpdateDto(string? code, string? name, string? cityId = null)
        {
            Code = code;
            Name = name;
            CityId = cityId;
        }
    }
}
