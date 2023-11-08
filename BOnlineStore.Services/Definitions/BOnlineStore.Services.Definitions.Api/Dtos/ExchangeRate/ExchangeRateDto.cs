using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Kur için ana dto
    /// </summary>
    public class ExchangeRateDto : EntityDto
    {
        /// <summary>
        /// Kur tarihi
        /// </summary>
        public DateTime? ExcangeRateDate { get; set; }
        /// <summary>
        /// Döviz kodu
        /// </summary>
        public string? CurrencyId { get; set; }
        /// <summary>
        /// Alış kuru
        /// </summary>
        public decimal? BuyingRate { get; set; }
        /// <summary>
        /// Satış kuru
        /// </summary>
        public decimal? SalesRate { get; set; }
        /// <summary>
        /// Efektif alış kuru
        /// </summary>
        public decimal? EffectiveBuyingRate { get; set; }
        /// <summary>
        /// Efektif satış kuru
        /// </summary>
        public decimal? EffectiveSalesRate { get; set; }
        /// <summary>
        /// Özel kur
        /// </summary>
        public decimal? SpecialRate01 { get; set; }
        /// <summary>
        /// Özel kur
        /// </summary>
        public decimal? SpecialRate02 { get; set; }

        public ExchangeRateDto(
            string id, DateTime? excangeRateDate = null, string? currencyId = null, decimal? buyingRate = null,
            decimal? salesRate = null, decimal? effectiveBuyingRate = null, decimal? effectiveSalesRate = null,
            decimal? specialRate01 = null, decimal? specialRate02 = null)
        {
            Id = id;
            ExcangeRateDate = excangeRateDate;
            CurrencyId = currencyId;
            BuyingRate = buyingRate;
            SalesRate = salesRate;
            EffectiveBuyingRate = effectiveBuyingRate;
            EffectiveSalesRate = effectiveSalesRate;
            SpecialRate01 = specialRate01;
            SpecialRate02 = specialRate02;
        }
    }
}
