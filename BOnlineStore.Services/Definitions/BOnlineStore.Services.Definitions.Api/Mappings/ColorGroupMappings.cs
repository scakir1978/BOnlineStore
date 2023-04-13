using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ColorGroupMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ColorGroup, ColorGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<ColorGroup, ColorGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<ColorGroup, ColorGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
