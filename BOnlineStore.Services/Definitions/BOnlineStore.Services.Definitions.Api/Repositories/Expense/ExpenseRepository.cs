using BOnlineStore.Localization;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(IContext context, IHttpContextAccessor httpContextAccessor, IValidator<Expense> validator, IStringLocalizer<Language> stringLocalizer)
            : base(context, httpContextAccessor, validator, stringLocalizer)
        { }
    }
}
