using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Banka
    /// </summary>
    public class BankDto : EntityDto
    {
        /// <summary>
        /// Banka kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Banka adı
        /// </summary>
        public string? Name { get; set; }

        public BankDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
