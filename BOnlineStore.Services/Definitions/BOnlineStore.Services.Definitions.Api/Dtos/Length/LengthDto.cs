using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class LengthDto : EntityDto
    {
        /// <summary>
        /// Boy kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Boy adı
        /// </summary>
        public string Name { get; set; }

        public LengthDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
