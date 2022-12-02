using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Services.Definitions.Api.Validations;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static BOnlineStore.Shared.Enums;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class ColorService : Service<Color, ColorDto, ColorCreateDto, ColorUpdateDto>, IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public ColorService(IColorRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer) : base(repository, mapper, stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
