using BOnlineStore.Services.Definitions.Api.Services;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services)
        {
            services.AddScoped<IModelGroupService, ModelGroupService>();
            services.AddScoped<IColorService, ColorService>();

            return services;
        }
    }
}
