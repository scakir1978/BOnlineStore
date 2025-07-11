using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class CurrencyCreateDto : EntityDto
    {
        /// <summary>
        /// Para birimi kodu (örn: USD, EUR, TRY)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Para birimi adı (örn: Amerikan Doları, Euro, Türk Lirası)
        /// </summary>
        public string Name { get; set; }

        public CurrencyCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
