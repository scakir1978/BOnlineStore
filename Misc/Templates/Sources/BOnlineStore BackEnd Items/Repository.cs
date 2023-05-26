using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using Microsoft.Extensions.Localization;

namespace $rootnamespace$.Repositories
{
    public class $className$Repository : Repository<$className$>, I$className$Repository
    {
        public $className$Repository(IContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, stringLocalizer)
    { }
}
}
