using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class FirmTypeCreateDto : EntityDto
    {
        /// <summary>
        /// Firma türü kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Firma türü adı
        /// </summary>
        public string Name { get; set; }

        public FirmTypeCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
