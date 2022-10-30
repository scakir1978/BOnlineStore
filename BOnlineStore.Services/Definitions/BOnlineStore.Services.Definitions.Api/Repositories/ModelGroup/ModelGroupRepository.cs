using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class ModelGroupRepository : Repository<ModelGroup>, IModelGroupRepository
    {
        public ModelGroupRepository(IContext context, IHttpContextAccessor httpContextAccessor, IValidator<ModelGroup> validator, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, validator, stringLocalizer)
        {}
    }
}
