using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class FormulaTypeService : Service<FormulaType, FormulaTypeDto, FormulaTypeCreateDto, FormulaTypeUpdateDto>, IFormulaTypeService
    {
        private readonly IFormulaTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public FormulaTypeService(IFormulaTypeRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer) : base(repository, mapper, stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
