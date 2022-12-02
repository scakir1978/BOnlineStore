using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api
{
    public class MappingConfigrations
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ModelGroup, ModelGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<ModelGroup, ModelGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<ModelGroup, ModelGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                config.CreateMap<Color, ColorDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Color, ColorCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Color, ColorUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            });

            return mappingConfig;
        }
    }
}
