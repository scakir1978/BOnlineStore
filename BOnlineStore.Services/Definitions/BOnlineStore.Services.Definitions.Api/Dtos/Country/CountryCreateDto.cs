using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    /// <summary>
    /// Yeni ülke eklemek için kullanılan dto
    /// </summary>
    public class CountryCreateDto : EntityDto
    {
        /// <summary>
        /// Ülke kodu.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Ülke adı.
        /// </summary>
        public string? Name { get; set; }

        public CountryCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
