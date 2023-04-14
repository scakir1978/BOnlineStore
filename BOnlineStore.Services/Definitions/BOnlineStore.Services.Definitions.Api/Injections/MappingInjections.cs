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
            services.AddSingleton(ExpenseMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(GlassMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(GlassGroupMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(LengthMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(RawMaterialGroupMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(RegionMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(TemplateMappings.RegisterMaps().CreateMapper());
            services.AddSingleton(UnitMappings.RegisterMaps().CreateMapper());

            return services;
        }
    }
}
