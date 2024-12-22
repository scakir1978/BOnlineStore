using BOnlineStore.Production.Api.Services;
using BOnlineStore.Services.Production.Api.Services;

namespace BOnlineStore.Services.Production.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFormulaTypeService, FormulaTypeService>();
            services.AddScoped<IFormulaService, FormulaService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();

            services.AddHttpClient<IDefinitionsService, DefinitionsService>(
                config => config.BaseAddress = new Uri(configuration["DefinitionsApiUrl"])
            );

            return services;
        }
    }
}
