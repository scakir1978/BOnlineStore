using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace $rootnamespace$.Dtos
{
    public class $safeitemname$ : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public $safeitemname$(string id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
