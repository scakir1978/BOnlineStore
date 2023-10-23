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
    public class ModelService : Service<Model, ModelDto, ModelCreateDto, ModelUpdateDto>, IModelService
    {
        public ModelService(
            IModelRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Model> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
