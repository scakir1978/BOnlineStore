using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Fiyat listesinin sadece master bilgilerinin yüklenmesini sağlayan dto
    /// </summary>
    public class PriceListMasterLoadDto : EntityDto
    {
        /// <summary>
        /// Fiyat listesi kodu.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Fiyatlistesi açıklaması veya adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi başlangıç tarihi
        /// </summary>
        public DateTime? FirstDate { get; set; }

        /// <summary>
        /// Fiyat listesi geçerlilik süresi bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
