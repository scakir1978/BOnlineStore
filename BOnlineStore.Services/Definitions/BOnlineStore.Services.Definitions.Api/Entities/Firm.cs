using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Firma tanımları.
    /// </summary>
    public class Firm : Entity
    {
        /// <summary>
        /// Firma kodu.
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Firma adı.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Ünvan
        /// </summary>
        public string Title { get; private set; }

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
        public string? TaxOffice { get; private set; }






        public Firm() : base()
        {
            Code = "";
            Name = "";
        }

        public Firm(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateFirm(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
