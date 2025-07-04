﻿using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Services.Definitions.Api.Services.Shared;

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
            services.AddScoped<IAssemblyPriceService, AssemblyPriceService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IGlassService, GlassService>();
            services.AddScoped<IGlassGroupService, GlassGroupService>();
            services.AddScoped<ILengthService, LengthService>();
            services.AddScoped<IRawMaterialService, RawMaterialService>();
            services.AddScoped<IRawMaterialGroupService, RawMaterialGroupService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IFirmTypeService, FirmTypeService>();
            services.AddScoped<IFirmService, FirmService>();
            services.AddScoped<IRecipeTypeService, RecipeTypeService>();
            services.AddScoped<IPanelService, PanelService>();
            services.AddScoped<IMeasurementAssemblyLimitsService, MeasurementAssemblyLimitsService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountyService, CountyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IPriceListService, PriceListService>();
            services.AddScoped<IDefinitionsRequestService, DefinitionsRequestService>();
            services.AddScoped<IDeliveryAdressService, DeliveryAdressService>();

            return services;
        }
    }
}
