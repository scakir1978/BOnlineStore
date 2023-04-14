using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class RawMaterialGroupMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RawMaterialGroup, RawMaterialGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<RawMaterialGroup, RawMaterialGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<RawMaterialGroup, RawMaterialGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
