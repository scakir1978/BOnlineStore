using System.Reflection;

namespace BOnlineStore.Services.Production.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddMappingInjections(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
