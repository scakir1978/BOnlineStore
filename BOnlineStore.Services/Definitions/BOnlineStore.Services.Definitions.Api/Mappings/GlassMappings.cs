using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class GlassMappings : Profile
    {
        public GlassMappings()
        {
            CreateMap<Glass, GlassDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Glass, GlassCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Glass, GlassUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
