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
    public class ModelGroupService : Service<ModelGroup, ModelGroupDto, ModelGroupCreateDto, ModelGroupUpdateDto>, IModelGroupService
    {
        public ModelGroupService(
            IModelGroupRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<ModelGroup> validator) : base(repository, mapper, stringLocalizer, validator)
        { }
        
    }
}
