using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ColorMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Color, ColorDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Color, ColorCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Color, ColorUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
