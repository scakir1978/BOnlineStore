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
    public class CountryService : Service<Country, CountryDto, CountryCreateDto, CountryUpdateDto>, ICountryService
    {
        public CountryService(
            ICountryRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Country> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
