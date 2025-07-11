using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ExpenseCreateDto : EntityDto
    {
        /// <summary>
        /// Gider kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gider adı
        /// </summary>
        public string Name { get; set; }

        public ExpenseCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
