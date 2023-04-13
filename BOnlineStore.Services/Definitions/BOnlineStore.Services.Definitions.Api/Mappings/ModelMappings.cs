using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ModelMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Model, ModelDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Model, ModelCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Model, ModelUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
