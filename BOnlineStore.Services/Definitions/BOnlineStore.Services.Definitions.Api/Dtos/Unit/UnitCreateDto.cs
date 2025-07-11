using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class UnitCreateDto : EntityDto
    {
        /// <summary>
        /// Birim kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Birim adı
        /// </summary>
        public string Name { get; set; }

        public UnitCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
