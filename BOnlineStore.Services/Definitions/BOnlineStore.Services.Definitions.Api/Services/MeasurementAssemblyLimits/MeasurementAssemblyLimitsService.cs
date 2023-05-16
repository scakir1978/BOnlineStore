using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class MeasurementAssemblyLimitsService : Service<MeasurementAssemblyLimits, MeasurementAssemblyLimitsDto, MeasurementAssemblyLimitsCreateDto, MeasurementAssemblyLimitsUpdateDto>, IMeasurementAssemblyLimitsService
    {
        private readonly IMeasurementAssemblyLimitsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public MeasurementAssemblyLimitsService(IMeasurementAssemblyLimitsRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer) : base(repository, mapper, stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
