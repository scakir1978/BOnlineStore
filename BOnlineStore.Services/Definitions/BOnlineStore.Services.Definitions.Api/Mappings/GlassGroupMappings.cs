using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class GlassGroupMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<GlassGroup, GlassGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<GlassGroup, GlassGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<GlassGroup, GlassGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
