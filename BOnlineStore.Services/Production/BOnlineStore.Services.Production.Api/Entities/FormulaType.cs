using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Entities
{
    public class FormulaType : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public FormulaType() : base()
        {
            Code = "";
            Name = "";
        }

        public FormulaType(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateFormulaType(string code, string name)
        {
            Code = code;
            Name = name;

        }
    }
}
