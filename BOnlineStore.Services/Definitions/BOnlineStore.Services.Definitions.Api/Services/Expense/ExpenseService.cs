using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class ExpenseService : Service<Expense, ExpenseDto, ExpenseCreateDto, ExpenseUpdateDto>, IExpenseService
    {
        public ExpenseService(
            IExpenseRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Expense> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
