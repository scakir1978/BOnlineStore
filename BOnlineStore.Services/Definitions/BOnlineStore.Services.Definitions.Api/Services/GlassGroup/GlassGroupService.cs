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
    public class GlassGroupService : Service<GlassGroup, GlassGroupDto, GlassGroupCreateDto, GlassGroupUpdateDto>, IGlassGroupService
    {
        public GlassGroupService(
            IGlassGroupRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<GlassGroup> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
