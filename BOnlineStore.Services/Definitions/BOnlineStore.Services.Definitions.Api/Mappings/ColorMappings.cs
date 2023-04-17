using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ColorMappings : Profile
    {
        public ColorMappings()
        {
            CreateMap<Color, ColorDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Color, ColorCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Color, ColorUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
