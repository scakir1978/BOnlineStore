using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Döviz kodu tanımlama
    public class Currency : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Currency() : base()
        {
            Code = "";
            Name = "";
        }

        public Currency(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateCurrency(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
