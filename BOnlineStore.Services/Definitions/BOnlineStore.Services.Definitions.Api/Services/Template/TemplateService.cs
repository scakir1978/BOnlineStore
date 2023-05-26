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
    public class TemplateService : Service<Template, TemplateDto, TemplateCreateDto, TemplateUpdateDto>, ITemplateService
    {
        public TemplateService(
            ITemplateRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Template> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
