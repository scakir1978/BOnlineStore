using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class GlassGroup : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public GlassGroup() : base()
        {
            Code = "";
            Name = "";
        }

        public GlassGroup(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateGlassGroup(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
