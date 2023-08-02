using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class PriceListRepository : Repository<PriceListMaster>, IPriceListRepository
    {
        public PriceListRepository(IContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, stringLocalizer)
        { }
    }
}
