using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Mappings;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddMappingInjections(this IServiceCollection services)
        {
            services.AddSingleton(AssemblerMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(ColorMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(ColorGroupMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(ModelMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(ModelGroupMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(CurrencyMappings.RegisterMaps().CreateMapper());
            return services;
        }
    }
}
