using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using $rootnamespace$.Entities;
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
