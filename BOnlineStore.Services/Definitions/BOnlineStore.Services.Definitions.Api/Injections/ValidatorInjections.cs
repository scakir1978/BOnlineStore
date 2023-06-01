using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Validations;
using FluentValidation;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddValidatorInjections(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ModelGroup>, ModelGroupValidator>();
            services.AddScoped<IValidator<Color>, ColorValidator>();
            services.AddScoped<IValidator<ColorGroup>, ColorGroupValidator>();
            services.AddScoped<IValidator<Model>, ModelValidator>();
            services.AddScoped<IValidator<Assembler>, AssemblerValidator>();
            services.AddScoped<IValidator<AssemblyPrice>, AssemblyPriceValidator>();
            services.AddScoped<IValidator<Currency>, CurrencyValidator>();
            services.AddScoped<IValidator<Expense>, ExpenseValidator>();
            services.AddScoped<IValidator<Glass>, GlassValidator>();
            services.AddScoped<IValidator<GlassGroup>, GlassGroupValidator>();
            services.AddScoped<IValidator<Length>, LengthValidator>();
            services.AddScoped<IValidator<RawMaterial>, RawMaterialValidator>();
            services.AddScoped<IValidator<RawMaterialGroup>, RawMaterialGroupValidator>();
            services.AddScoped<IValidator<Region>, RegionValidator>();
            services.AddScoped<IValidator<Template>, TemplateValidator>();
            services.AddScoped<IValidator<Unit>, UnitValidator>();
            services.AddScoped<IValidator<Bank>, BankValidator>();
            services.AddScoped<IValidator<FirmType>, FirmTypeValidator>();
            services.AddScoped<IValidator<RecipeType>, RecipeTypeValidator>();
            services.AddScoped<IValidator<Panel>, PanelValidator>();
            services.AddScoped<IValidator<MeasurementAssemblyLimits>, MeasurementAssemblyLimitsValidator>();
            services.AddScoped<IValidator<ExchangeRate>, ExchangeRateValidator>();
            services.AddScoped<IValidator<City>, CityValidator>();
            services.AddScoped<IValidator<District>, DistrictValidator>();


            return services;
        }
    }
}
