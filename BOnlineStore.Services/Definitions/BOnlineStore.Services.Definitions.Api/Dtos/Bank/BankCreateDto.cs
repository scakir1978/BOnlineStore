using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Banka eklemek kullanılan dto
    /// </summary>
    public class BankCreateDto : EntityDto
    {
        /// <summary>
        /// Banka kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Banka adı.
        /// </summary>
        public string? Name { get; set; }

        public BankCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
