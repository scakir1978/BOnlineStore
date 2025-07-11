using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ExpenseDto : EntityDto
    {
        /// <summary>
        /// Gider kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gider adı
        /// </summary>
        public string Name { get; set; }

        public ExpenseDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
