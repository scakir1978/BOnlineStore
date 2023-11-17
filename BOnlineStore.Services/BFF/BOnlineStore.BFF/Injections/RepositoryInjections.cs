namespace BOnlineStore.BFF.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
