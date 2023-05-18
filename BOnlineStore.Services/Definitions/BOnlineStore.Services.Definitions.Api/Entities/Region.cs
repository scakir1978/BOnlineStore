using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Bölge tanımlama
    /// <summary>
    /// Bölge
    /// </summary>
    public class Region : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Region() : base()
        {
            Code = "";
            Name = "";
        }

        public Region(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateRegion(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
