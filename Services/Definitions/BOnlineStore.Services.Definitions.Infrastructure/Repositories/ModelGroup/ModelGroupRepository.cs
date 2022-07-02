using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Domain.Entities;
using BOnlineStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Services.Definitions.Infrastructure.Repositories
{
    public class ModelGroupRepository : Repository<ModelGroup>, IModelGroupRepository
    {
        public ModelGroupRepository(IContext context) : base(context)
        {}
    }
}
