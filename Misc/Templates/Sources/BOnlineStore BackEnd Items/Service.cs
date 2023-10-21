using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace $rootnamespace$.Services
{
    public class $safeitemname$ : Service<$className$, $className$Dto, $className$CreateDto, $className$UpdateDto>, I$className$Service
    {
        public $safeitemname$(
            I$className$Repository repository,
            IMapper mapper, 
            IStringLocalizer<Language> stringLocalizer,
            IValidator<$className$> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
