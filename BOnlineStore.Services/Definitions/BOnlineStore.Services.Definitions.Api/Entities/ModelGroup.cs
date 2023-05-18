using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Model grubu
    /// </summary>
    public class ModelGroup : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public ModelGroup() : base()
        {
            Code = "";
            Name = "";
        }

        public ModelGroup(Guid tenantId, string id, string code, string name) : base(tenantId, id)
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
