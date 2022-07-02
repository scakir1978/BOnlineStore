using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; private set ; }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
