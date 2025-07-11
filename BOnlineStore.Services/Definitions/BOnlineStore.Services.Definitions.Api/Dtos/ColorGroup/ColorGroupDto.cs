using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorGroupDto : EntityDto
    {
        /// <summary>
        /// Renk grubu kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Renk grubu adı
        /// </summary>
        public string Name { get; set; }

        public ColorGroupDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
