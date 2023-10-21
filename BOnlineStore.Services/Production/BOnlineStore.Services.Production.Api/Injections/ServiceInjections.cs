using BOnlineStore.Services.Production.Api.Services;

namespace BOnlineStore.Services.Production.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services)
        {
            services.AddScoped<IFormulaTypeService, FormulaTypeService>();
            services.AddScoped<IFormulaService, FormulaService>();

            return services;
        }
    }
}
