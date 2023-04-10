using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Montör tanımlama
    public class Assembler : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Assembler() : base()
        {
            Code = "";
            Name = "";
        }

        public Assembler(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateAssembler(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
