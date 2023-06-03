using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class DistrictMappings : Profile
    {
        public DistrictMappings()
        {
            CreateMap<District, DistrictDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<District, DistrictCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<District, DistrictUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

