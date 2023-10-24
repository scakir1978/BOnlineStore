using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class RawMaterialGroupMappings : Profile
    {
        public RawMaterialGroupMappings()
        {
            CreateMap<RawMaterialGroup, RawMaterialGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RawMaterialGroup, RawMaterialGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RawMaterialGroup, RawMaterialGroupUpdateDto>().DisableCtorValidation().ReverseMap()
                .DisableCtorValidation()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
