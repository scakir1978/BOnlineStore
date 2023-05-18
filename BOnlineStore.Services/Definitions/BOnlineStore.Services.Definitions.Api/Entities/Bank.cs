using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api
{
    /// <summary>
    /// Banka
    /// </summary>
    public class Bank : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Bank() : base()
        {
            Code = "";
            Name = "";
        }

        public Bank(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateBank(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
