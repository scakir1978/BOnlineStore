using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class RegionMappings : Profile
    {
        public RegionMappings()
        {
            CreateMap<Region, RegionDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Region, RegionCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Region, RegionUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
