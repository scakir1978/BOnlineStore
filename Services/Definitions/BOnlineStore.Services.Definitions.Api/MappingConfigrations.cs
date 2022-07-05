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
                config.CreateMap<ModelGroup, ModelGroupDto>().ReverseMap();
                config.CreateMap<ModelGroup, ModelGroupCreateDto>().ReverseMap();
                config.CreateMap<ModelGroup, ModelGroupUpdateDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
