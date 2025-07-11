using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class LengthCreateDto : EntityDto
    {
        /// <summary>
        /// Boy kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Boy adı
        /// </summary>
        public string Name { get; set; }

        public LengthCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
