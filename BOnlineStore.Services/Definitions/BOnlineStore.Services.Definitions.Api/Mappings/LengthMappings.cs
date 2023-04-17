using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class LengthMappings : Profile
    {
        public LengthMappings()
        {
            CreateMap<Length, LengthDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Length, LengthCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Length, LengthUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
