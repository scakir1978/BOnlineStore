using BOnlineStore.BFF.Api.Services.Definitions;
using BOnlineStore.BFF.Api.Services.Production;

namespace BOnlineStore.BFF.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWorkOrderService, WorkOrderService>(
                config => config.BaseAddress = new Uri(configuration["ProductionApiUrl"])
            );

            services.AddHttpClient<IDefinitionsService, DefinitionsService>(
                config => config.BaseAddress = new Uri(configuration["DefinitionsApiUrl"])
            );

            return services;
        }
    }
}
