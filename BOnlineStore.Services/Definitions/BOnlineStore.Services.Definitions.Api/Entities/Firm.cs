using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class Firm : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

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
