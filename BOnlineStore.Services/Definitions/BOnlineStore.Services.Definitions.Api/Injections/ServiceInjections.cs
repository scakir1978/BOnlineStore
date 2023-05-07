using BOnlineStore.Services.Definitions.Api.Services;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services)
        {
            services.AddScoped<IModelGroupService, ModelGroupService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IColorGroupService, ColorGroupService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IAssemblerService, AssemblerService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IGlassService, GlassService>();
            services.AddScoped<IGlassGroupService, GlassGroupService>();
            services.AddScoped<ILengthService, LengthService>();
            services.AddScoped<IRawMaterialGroupService, RawMaterialGroupService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IFirmTypeService, FirmTypeService>();

            return services;
        }
    }
}
