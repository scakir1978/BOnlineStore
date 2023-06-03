using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Ülke tanımlama.
    /// </summary>
    public class Country : Entity
    {
        /// <summary>
        /// Ülke kodu.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Ülke adı.
        /// </summary>
        public string Name { get; private set; }

        public Country() : base()
        {
            Code = "";
            Name = "";
        }

        public Country(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateCountry(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
