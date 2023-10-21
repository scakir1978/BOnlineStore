using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class FormulaService : Service<Formula, FormulaDto, FormulaCreateDto, FormulaUpdateDto>, IFormulaService
    {
        public FormulaService(
            IFormulaRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Formula> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
