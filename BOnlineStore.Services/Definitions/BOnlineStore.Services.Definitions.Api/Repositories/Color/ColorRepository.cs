using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(IContext context, IHttpContextAccessor httpContextAccessor, IValidator<Color> validator, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, validator, stringLocalizer)
        { }
    }
}
