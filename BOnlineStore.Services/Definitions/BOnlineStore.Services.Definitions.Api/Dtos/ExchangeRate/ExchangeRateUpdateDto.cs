namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Kur için güncelleme dto'su
    /// </summary>
    public class ExchangeRateUpdateDto
    {
        /// <summary>
        /// Kur tarihi
        /// </summary>
        public DateTime? ExcangeRateDate { get; private set; }
        /// <summary>
        /// Döviz kodu
        /// </summary>
        public string? CurrencyId { get; private set; }
        /// <summary>
        /// Alış kuru
        /// </summary>
        public decimal? BuyingRate { get; private set; }
        /// <summary>
        /// Satış kuru
        /// </summary>
        public decimal? SalesRate { get; private set; }
        /// <summary>
        /// Efektif alış kuru
        /// </summary>
        public decimal? EffectiveBuyingRate { get; private set; }
        /// <summary>
        /// Efektif satış kuru
        /// </summary>
        public decimal? EffectiveSalesRate { get; private set; }
        /// <summary>
        /// Özel kur
        /// </summary>
        public decimal? SpecialRate01 { get; private set; }
        /// <summary>
        /// Özel kur
        /// </summary>
        public decimal? SpecialRate02 { get; private set; }

        public ExchangeRateUpdateDto(DateTime? excangeRateDate = null, string? currencyId = null, decimal? buyingRate = null, decimal? salesRate = null, decimal? effectiveBuyingRate = null, decimal? effectiveSalesRate = null, decimal? specialRate01 = null, decimal? specialRate02 = null)
        {
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
