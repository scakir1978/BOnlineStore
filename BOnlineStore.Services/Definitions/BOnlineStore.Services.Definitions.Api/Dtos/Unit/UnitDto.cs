using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class UnitDto : EntityDto
    {
        /// <summary>
        /// Birim kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Birim adı
        /// </summary>
        public string Name { get; set; }

        public UnitDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
