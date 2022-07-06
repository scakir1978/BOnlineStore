using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class ModelGroupRepository : Repository<ModelGroup>, IModelGroupRepository
    {
        public ModelGroupRepository(IContext context) : base(context)
        {}
    }
}
