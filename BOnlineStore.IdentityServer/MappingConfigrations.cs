using AutoMapper;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer
{
    public class MappingConfigrations
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Tenant, TenantDto>().ReverseMap();
                config.CreateMap<Tenant, TenantCreateDto>().ReverseMap();
                config.CreateMap<Tenant, TenantUpdateDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
