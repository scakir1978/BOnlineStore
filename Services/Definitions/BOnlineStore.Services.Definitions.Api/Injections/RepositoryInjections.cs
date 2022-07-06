using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddScoped<IModelGroupRepository, ModelGroupRepository>();           

            return services;
        }
    }
}
