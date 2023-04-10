using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Gider tanımlama
    public class Expense : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Expense() : base()
        {
            Code = "";
            Name = "";
        }

        public Expense(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateExpense(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
