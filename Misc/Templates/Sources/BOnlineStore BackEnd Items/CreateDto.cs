using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace $rootnamespace$.Dtos
{
    public class $safeitemname$ : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }        

        public $safeitemname$(string code, string name)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
        }
    }
}
