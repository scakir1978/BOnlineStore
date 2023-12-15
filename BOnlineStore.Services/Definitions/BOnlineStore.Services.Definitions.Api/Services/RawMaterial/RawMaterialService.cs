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
    public class RawMaterialService : Service<RawMaterial, RawMaterialDto, RawMaterialCreateDto, RawMaterialUpdateDto>, IRawMaterialService
    {
        private readonly IMapper _mapper;

        public RawMaterialService(
            IRawMaterialRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<RawMaterial> validator) : base(repository, mapper, stringLocalizer, validator)
        {
            _mapper = mapper; ;
        }

        public List<RawMaterialDto> LoadFromList(List<string> rawMaterialIds)
        {
            return _mapper.Map<List<RawMaterialDto>>(Load(x => rawMaterialIds.Contains(x.Id)).ToList());
        }
    }
}
