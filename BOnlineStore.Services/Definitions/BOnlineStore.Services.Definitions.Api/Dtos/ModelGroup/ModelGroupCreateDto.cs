using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelGroupCreateDto : EntityDto
    {
        /// <summary>
        /// Model grubu kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Model grubu adı
        /// </summary>
        public string? Name { get; set; }

        public ModelGroupCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
