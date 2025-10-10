using AutoMapper;
using BOnlineStore.IdentityServer.Mappings;

namespace BOnlineStore.IdentityServer
{
    public class MappingConfigrations
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // User ve Tenant mappings için profil kullan
                config.AddProfile<UserMappingProfile>();
            });

            return mappingConfig;
        }
    }
}
