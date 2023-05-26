using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class District : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public District() : base()
        {
            Code = "";
            Name = "";
        }

        public District(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateDistrict(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
