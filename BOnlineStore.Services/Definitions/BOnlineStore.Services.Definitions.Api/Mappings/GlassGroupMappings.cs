using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class GlassGroupMappings : Profile
    {
        public GlassGroupMappings()
        {
            CreateMap<GlassGroup, GlassGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<GlassGroup, GlassGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<GlassGroup, GlassGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
