using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Şablon tanımlama
    /// <summary>
    /// Şablon
    /// </summary>
    public class Template : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Template() : base()
        {
            Code = "";
            Name = "";
        }

        public Template(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateTemplate(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
