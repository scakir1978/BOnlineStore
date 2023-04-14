using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class UnitMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Unit, UnitDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Unit, UnitCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Unit, UnitUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
