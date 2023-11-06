using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Production.Api.Entities;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(IContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, stringLocalizer)
        { }
    }
}
