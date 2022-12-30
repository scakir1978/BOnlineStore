using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Repositories
{
    public class FormulaTypeRepository : Repository<FormulaType>, IFormulaTypeRepository
    {
        public FormulaTypeRepository(IContext context, IHttpContextAccessor httpContextAccessor, IValidator<FormulaType> validator, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, validator, stringLocalizer)
        { }
    }
}
