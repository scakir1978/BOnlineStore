using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class RawMaterialGroupRepository : Repository<RawMaterialGroup>, IRawMaterialGroupRepository
    {
        public RawMaterialGroupRepository(IContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer) : base(context, httpContextAccessor, stringLocalizer)
        { }
    }
}
