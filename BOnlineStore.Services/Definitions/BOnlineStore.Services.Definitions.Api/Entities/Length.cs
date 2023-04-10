using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Boy tanımlama
    public class Length : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Length() : base()
        {
            Code = "";
            Name = "";
        }

        public Length(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateLength(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
