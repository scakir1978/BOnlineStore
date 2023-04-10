using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Hammadde grubu tanımlama
    public class RawMaterialGroup : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public RawMaterialGroup() : base()
        {
            Code = "";
            Name = "";
        }

        public RawMaterialGroup(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateRawMaterialGroup(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
