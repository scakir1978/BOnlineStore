using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class CurrencyDto : EntityDto
    {
        /// <summary>
        /// Para birimi kodu (örn: USD, EUR, TRY)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Para birimi adı (örn: Amerikan Doları, Euro, Türk Lirası)
        /// </summary>
        public string Name { get; set; }

        public CurrencyDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
