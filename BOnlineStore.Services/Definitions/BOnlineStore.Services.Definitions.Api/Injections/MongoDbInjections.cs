using BOnlineStore.MongoDb.GenericRepository;
using Microsoft.Extensions.Options;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddMongoDbConfigurationAndInjections(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddScoped<IContext, Context>();

            return services;
        }
    }
}
