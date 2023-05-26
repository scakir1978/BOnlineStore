using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace $rootnamespace$.Entities
{
    public class $className$ : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public $className$() : base()
        {
            Code = "";
            Name = "";
        }

        public $className$(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void Update$className$(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
