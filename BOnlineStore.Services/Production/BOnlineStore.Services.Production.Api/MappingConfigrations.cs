using AutoMapper;

namespace BOnlineStore.Services.Production.Api
{
    public class MappingConfigrations
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

            });

            return mappingConfig;
        }
    }
}
