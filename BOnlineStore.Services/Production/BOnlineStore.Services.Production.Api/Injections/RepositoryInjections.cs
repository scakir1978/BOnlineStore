using BOnlineStore.Services.Production.Api.Repositories;

namespace BOnlineStore.Services.Production.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IFormulaTypeRepository, FormulaTypeRepository>();

            return services;
        }
    }
}
