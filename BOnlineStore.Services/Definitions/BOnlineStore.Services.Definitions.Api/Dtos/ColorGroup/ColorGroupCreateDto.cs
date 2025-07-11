using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ColorGroupCreateDto : EntityDto
    {
        /// <summary>
        /// Renk grubu kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Renk grubu adı
        /// </summary>
        public string Name { get; set; }

        public ColorGroupCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
