using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class GlassGroupCreateDto : EntityDto
    {
        /// <summary>
        /// Cam grubu kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Cam grubu adı
        /// </summary>
        public string Name { get; set; }

        public GlassGroupCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
