using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class RawMaterialRepository : Repository<RawMaterial>, IRawMaterialRepository
    {
        public RawMaterialRepository(IContext context, IHttpContextAccessor httpContextAccessor, IValidator<RawMaterial> validator, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, validator, stringLocalizer)
        { }
    }
}
