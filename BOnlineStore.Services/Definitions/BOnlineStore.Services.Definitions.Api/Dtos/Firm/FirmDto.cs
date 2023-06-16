using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
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
        public string? FirmTypeId { get; private set; }

        /// <summary>
        /// Ünvan
        /// </summary>
        public string? Title { get; private set; }

        /// <summary>
        /// Yetkili kişi.
        /// </summary>
        public string? CompetentPerson { get; private set; }

        /// <summary>
        /// Müşteri temsilcisi.
        /// </summary>
        public string? CustomerRepresentative { get; private set; }

        /// <summary>
        /// Vergi numarası.
        /// </summary>
        public string? TaxNumber { get; private set; }

        /// <summary>
        /// Vergi dairesi.
        /// </summary>
        public string? TaxAdministration { get; private set; }

        /// <summary>
        /// Ülke Id
        /// </summary>
        public string? CountryId { get; private set; }

        /// <summary>
        /// Bölge Id
        /// </summary>
        public string? RegionId { get; private set; }

        /// <summary>
        /// Şehir Id
        /// </summary>
        public string? CityId { get; private set; }

        /// <summary>
        /// İlçe Id
        /// </summary>
        public string? CountyId { get; private set; }

        /// <summary>
        /// Mahalle Id
        /// </summary>
        public string? DistrictId { get; private set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string? Adress { get; private set; }

        /// <summary>
        /// Posta Kodu
        /// </summary>
        public string? PostalCode { get; private set; }

        /// <summary>
        /// Cep Telefon Numarası-1
        /// </summary>
        public string? CellPhoneNumber1 { get; private set; }

        /// <summary>
        /// Cep Telefon Numarası-2
        /// </summary>
        public string? CellPhoneNumber2 { get; private set; }

        /// <summary>
        /// Telefon Numarası-1
        /// </summary>
        public string? TelephoneNumber1 { get; private set; }

        /// <summary>
        /// Telefon Numarası-2
        /// </summary>
        public string? TelephoneNumber2 { get; private set; }

        /// <summary>
        /// Telefon Numarası-3
        /// </summary>
        public string? TelephoneNumber3 { get; private set; }

        /// <summary>
        /// Fax Numarası
        /// </summary>
        public string? FaxNumber { get; private set; }

        /// <summary>
        /// E-Mail Adresi
        /// </summary>
        public string? EMail { get; private set; }

        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string? PaymentMethod { get; private set; }

        /// <summary>
        /// Cari Hareket Türü
        /// </summary>
        public string? TransactionType { get; private set; }

        /// <summary>
        /// Özel Kod
        /// </summary>
        public string? SpecialCode { get; private set; }

        /// <summary>
        /// Fiyat Listesi Id
        /// </summary>
        public string? PriceListId { get; private set; }

        /// <summary>
        /// Risk Limiti
        /// </summary>
        public decimal? RiskLimit { get; private set; }

        /// <summary>
        /// Peşin İskonto - 1
        /// </summary>
        public decimal? AdvanceDiscount1 { get; private set; }

        /// <summary>
        /// Peşin İskonto - 2
        /// </summary>
        public decimal? AdvanceDiscount2 { get; private set; }

        /// <summary>
        /// Vadeli İskonto
        /// </summary>
        public decimal? ForwardDiscount { get; private set; }

        /// <summary>
        /// Valör
        /// </summary>
        public int? Valor { get; private set; }

        /// <summary>
        /// Tanımlanana KDV oranı fatura etkilesin mi?
        /// </summary>
        public bool? VatRatioForInvoice { get; private set; }

        /// <summary>
        /// KDV Oranı
        /// </summary>
        public decimal? VatRatio { get; private set; }

        /// <summary>
        /// Açıklamalar
        /// </summary>
        public string? Explanations { get; private set; }

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
