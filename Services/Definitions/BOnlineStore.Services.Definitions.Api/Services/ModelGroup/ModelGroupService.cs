using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Services.Definitions.Api.Validations;
using FluentValidation;
using static BOnlineStore.Shared.Enums;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class ModelGroupService : Service<ModelGroup,ModelGroupDto,ModelGroupCreateDto,ModelGroupUpdateDto> , IModelGroupService
    {
        private readonly IModelGroupRepository _repository;
        private readonly IMapper _mapper;

        public ModelGroupService(IModelGroupRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;            
        }

        public override IValidator ServiceValidator(ValidationTypeEnum validationType)
        {
            if (validationType == ValidationTypeEnum.Update) 
                return new ModelGroupUpdateValidation();

            return new ModelGroupCreateValidation();
        }
    }
}
