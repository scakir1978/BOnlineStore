using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IModelGroupRepository, ModelGroupRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IColorGroupRepository, ColorGroupRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IAssemblerRepository, AssemblerRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IGlassRepository, GlassRepository>();
            services.AddScoped<IGlassGroupRepository, GlassGroupRepository>();
            services.AddScoped<ILengthRepository, LengthRepository>();
            services.AddScoped<IRawMaterialGroupRepository, RawMaterialGroupRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            return services;
        }
    }
}
