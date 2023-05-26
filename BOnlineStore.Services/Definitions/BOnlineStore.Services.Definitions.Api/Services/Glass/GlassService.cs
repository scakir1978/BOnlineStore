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
    public class GlassService : Service<Glass, GlassDto, GlassCreateDto, GlassUpdateDto>, IGlassService
    {
        public GlassService(
            IGlassRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Glass> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
