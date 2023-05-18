using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Birim
    /// </summary>
    public class Unit : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Unit() : base()
        {
            Code = "";
            Name = "";
        }

        public Unit(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateUnit(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
