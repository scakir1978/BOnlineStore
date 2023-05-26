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
    public class FormulaTypeService : Service<FormulaType, FormulaTypeDto, FormulaTypeCreateDto, FormulaTypeUpdateDto>, IFormulaTypeService
    {
        private readonly IFormulaTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public FormulaTypeService(
            IFormulaTypeRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<FormulaType> validator) : base(repository, mapper, stringLocalizer, validator)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
