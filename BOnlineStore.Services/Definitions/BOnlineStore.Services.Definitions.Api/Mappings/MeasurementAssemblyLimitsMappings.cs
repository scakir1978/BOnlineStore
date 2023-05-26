using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class MeasurementAssemblyLimitsMappings : Profile
    {
        public MeasurementAssemblyLimitsMappings()
        {
            CreateMap<MeasurementAssemblyLimits, MeasurementAssemblyLimitsDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<MeasurementAssemblyLimits, MeasurementAssemblyLimitsCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<MeasurementAssemblyLimits, MeasurementAssemblyLimitsUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

