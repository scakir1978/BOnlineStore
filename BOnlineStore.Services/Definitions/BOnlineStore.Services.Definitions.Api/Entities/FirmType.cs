using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api
{
    /// <summary>
    /// Firma tipi (Cari tip)
    /// </summary>
    public class FirmType : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public FirmType() : base()
        {
            Code = "";
            Name = "";
        }

        public FirmType(Guid tenantId, string id, string code, string name) : base(tenantId, id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateFirmType(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
