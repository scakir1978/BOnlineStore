using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    public class FormulaTypeDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public FormulaTypeDto(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
