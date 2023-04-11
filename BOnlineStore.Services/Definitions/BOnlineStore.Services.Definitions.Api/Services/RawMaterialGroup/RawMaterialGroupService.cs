using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class RawMaterialGroupService : Service<RawMaterialGroup, RawMaterialGroupDto, RawMaterialGroupCreateDto, RawMaterialGroupUpdateDto>, IRawMaterialGroupService
    {
        private readonly IRawMaterialGroupRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public RawMaterialGroupService(IRawMaterialGroupRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer) : base(repository, mapper, stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
