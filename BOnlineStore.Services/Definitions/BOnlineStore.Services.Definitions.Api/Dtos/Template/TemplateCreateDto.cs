using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class TemplateCreateDto : EntityDto
    {
        /// <summary>
        /// Şablon kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Şablon adı
        /// </summary>
        public string Name { get; set; }

        public TemplateCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
