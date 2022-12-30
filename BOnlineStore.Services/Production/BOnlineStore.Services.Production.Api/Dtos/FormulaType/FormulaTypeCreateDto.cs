using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    public class FormulaTypeCreateDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public FormulaTypeCreateDto(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
