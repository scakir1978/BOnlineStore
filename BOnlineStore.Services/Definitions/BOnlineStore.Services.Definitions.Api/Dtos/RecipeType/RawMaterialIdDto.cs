namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Kullanılan hammaddenin id bilgisi
    /// </summary>
    public class RawMaterialIdDto
    {
        public string? Id { get; set; }
    }

    /// <summary>
    /// Modelin cam üretiminde kullanılan en ve boy hammadde bilgileri
    /// </summary>
    public class GlassRawMaterialIdDto
    {
        /// <summary>
        /// Camın en bilgisi için kullanılan hammadder idsi
        /// </summary>
        public string? WidthMaterialId { get; set; }

        /// <summary>
        /// Camın boy bilgisi için kullanılan hammadder idsi
        /// </summary>
        public string? LengthMaterialId { get; set; }
    }
}
