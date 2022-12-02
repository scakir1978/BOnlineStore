using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class ColorGroup : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public ColorGroup() : base()
        {
            Code = "";
            Name = "";
        }

        public ColorGroup(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateColorGroup(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
