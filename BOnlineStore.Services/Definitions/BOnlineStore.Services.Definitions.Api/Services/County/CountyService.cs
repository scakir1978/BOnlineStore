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
    public class CountyService : Service<County, CountyDto, CountyCreateDto, CountyUpdateDto>, ICountyService
    {
        public CountyService(
            ICountyRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<County> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
