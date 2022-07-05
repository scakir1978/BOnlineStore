using BOnlineStore.Shared.Entities;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class ModelGroup : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public ModelGroup(Guid id, string code, string name) : base(id)
        {
            Code = code;
            Name = name;
        }

        public void UpdateModelGroup(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
