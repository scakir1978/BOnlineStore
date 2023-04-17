using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ColorGroupMappings : Profile
    {
        public ColorGroupMappings()
        {
            CreateMap<ColorGroup, ColorGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ColorGroup, ColorGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ColorGroup, ColorGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
