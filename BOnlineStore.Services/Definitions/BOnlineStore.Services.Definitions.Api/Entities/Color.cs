using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class Color : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Color() : base()
        {
            Code = "";
            Name = "";
        }

        public Color(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateModelGroup(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
