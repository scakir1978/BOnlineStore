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
    public class RegionService : Service<Region, RegionDto, RegionCreateDto, RegionUpdateDto>, IRegionService
    {
        public RegionService(IRegionRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer, IValidator<Region> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
