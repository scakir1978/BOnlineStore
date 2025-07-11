using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class TemplateDto : EntityDto
    {
        /// <summary>
        /// Şablon kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Şablon adı
        /// </summary>
        public string Name { get; set; }

        public TemplateDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
