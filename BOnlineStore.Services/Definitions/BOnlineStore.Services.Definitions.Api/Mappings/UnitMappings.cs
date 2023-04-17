using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class UnitMappings : Profile
    {
        public UnitMappings()
        {
            CreateMap<Unit, UnitDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Unit, UnitCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Unit, UnitUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
