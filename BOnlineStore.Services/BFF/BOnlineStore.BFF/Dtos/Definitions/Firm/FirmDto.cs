using BOnlineStore.Shared.Entity;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    /// <summary>
    /// Firma dto
    /// </summary>
    public class FirmDto : EntityDto
    {
        /// <summary>
        /// Firma Kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Firma Adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Firma Tipi Id
        /// </summary>
        public string? FirmTypeId { get; set; }

        /// <summary>
        /// Ünvan
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Yetkili kişi.
        /// </summary>
        public string? CompetentPerson { get; set; }

        /// <summary>
        /// Müşteri temsilcisi.
        /// </summary>
        public string? CustomerRepresentative { get; set; }

        /// <summary>
        /// Vergi numarası.
        /// </summary>
        public string? TaxNumber { get; set; }

        /// <summary>
        /// Vergi dairesi.
        /// </summary>
        public string? TaxAdministration { get; set; }

        /// <summary>
        /// Ülke Id
        /// </summary>
        public string? CountryId { get; set; }

        /// <summary>
        /// Bölge Id
        /// </summary>
        public string? RegionId { get; set; }

        /// <summary>
        /// Şehir Id
        /// </summary>
        public string? CityId { get; set; }

        /// <summary>
        /// İlçe Id
        /// </summary>
        public string? CountyId { get; set; }

        /// <summary>
        /// Mahalle Id
        /// </summary>
        public string? DistrictId { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string? Adress { get; set; }

        /// <summary>
        /// Posta Kodu
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Cep Telefon Numarası-1
        /// </summary>
        public string? CellPhoneNumber1 { get; set; }

        /// <summary>
        /// Cep Telefon Numarası-2
        /// </summary>
        public string? CellPhoneNumber2 { get; set; }

        /// <summary>
        /// Telefon Numarası-1
        /// </summary>
        public string? TelephoneNumber1 { get; set; }

        /// <summary>
        /// Telefon Numarası-2
        /// </summary>
        public string? TelephoneNumber2 { get; set; }

        /// <summary>
        /// Telefon Numarası-3
        /// </summary>
        public string? TelephoneNumber3 { get; set; }

        /// <summary>
        /// Fax Numarası
        /// </summary>
        public string? FaxNumber { get; set; }

        /// <summary>
        /// E-Mail Adresi
        /// </summary>
        public string? EMail { get; set; }

        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string? PaymentMethod { get; set; }

        /// <summary>
        /// Cari Hareket Türü
        /// </summary>
        public string? TransactionType { get; set; }

        /// <summary>
        /// Özel Kod
        /// </summary>
        public string? SpecialCode { get; set; }

        /// <summary>
        /// Fiyat Listesi Id
        /// </summary>
        public string? PriceListId { get; set; }

        /// <summary>
        /// Risk Limiti
        /// </summary>
        public decimal? RiskLimit { get; set; }

        /// <summary>
        /// Peşin İskonto - 1
        /// </summary>
        public decimal? AdvanceDiscount1 { get; set; }

        /// <summary>
        /// Peşin İskonto - 2
        /// </summary>
        public decimal? AdvanceDiscount2 { get; set; }

        /// <summary>
        /// Vadeli İskonto
        /// </summary>
        public decimal? ForwardDiscount { get; set; }

        /// <summary>
        /// Valör
        /// </summary>
        public int? Valor { get; set; }

        /// <summary>
        /// Tanımlanana KDV oranı fatura etkilesin mi?
        /// </summary>
        public bool? VatRatioForInvoice { get; set; }

        /// <summary>
        /// KDV Oranı
        /// </summary>
        public decimal? VatRatio { get; set; }

        /// <summary>
        /// Açıklamalar
        /// </summary>
        public string? Explanations { get; set; }

        public FirmDto(
            string id, string code, string name, string? firmTypeId = null, string? title = null,
            string? competentPerson = null, string? customerRepresentative = null, string? taxNumber = null,
            string? taxAdministration = null, string? countryId = null, string? regionId = null, string? cityId = null,
            string? countyId = null, string? districtId = null, string? adress = null, string? postalCode = null,
            string? cellPhoneNumber1 = null, string? cellPhoneNumber2 = null, string? telephoneNumber1 = null,
            string? telephoneNumber2 = null, string? telephoneNumber3 = null, string? faxNumber = null, string? eMail = null,
            string? paymentMethod = null, string? transactionType = null, string? specialCode = null,
            string? priceListId = null, decimal? riskLimit = null, decimal? advanceDiscount1 = null,
            decimal? advanceDiscount2 = null, decimal? forwardDiscount = null, int? valor = null,
            bool? vatRatioForInvoice = null, decimal? vatRatio = null, string? explanations = null)
        {
            Id = id;
            Code = code;
            Name = name;
            FirmTypeId = firmTypeId;
            Title = title;
            CompetentPerson = competentPerson;
            CustomerRepresentative = customerRepresentative;
            TaxNumber = taxNumber;
            TaxAdministration = taxAdministration;
            CountryId = countryId;
            RegionId = regionId;
            CityId = cityId;
            CountyId = countyId;
            DistrictId = districtId;
            Adress = adress;
            PostalCode = postalCode;
            CellPhoneNumber1 = cellPhoneNumber1;
            CellPhoneNumber2 = cellPhoneNumber2;
            TelephoneNumber1 = telephoneNumber1;
            TelephoneNumber2 = telephoneNumber2;
            TelephoneNumber3 = telephoneNumber3;
            FaxNumber = faxNumber;
            EMail = eMail;
            PaymentMethod = paymentMethod;
            TransactionType = transactionType;
            SpecialCode = specialCode;
            PriceListId = priceListId;
            RiskLimit = riskLimit;
            AdvanceDiscount1 = advanceDiscount1;
            AdvanceDiscount2 = advanceDiscount2;
            ForwardDiscount = forwardDiscount;
            Valor = valor;
            VatRatioForInvoice = vatRatioForInvoice;
            VatRatio = vatRatio;
            Explanations = explanations;
        }
    }
}
