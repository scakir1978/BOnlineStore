using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Services.Definitions.Api.Validations;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class ColorService : Service<Color, ColorDto, ColorCreateDto, ColorUpdateDto>, IColorService
    {
        public ColorService(
            IColorRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Color> validator) : base(repository, mapper, stringLocalizer, validator)
        { }


    }
}
