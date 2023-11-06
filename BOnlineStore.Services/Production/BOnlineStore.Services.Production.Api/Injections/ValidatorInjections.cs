using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Validations;
using FluentValidation;

namespace BOnlineStore.Services.Production.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddValidatorInjections(this IServiceCollection services)
        {

            services.AddScoped<IValidator<FormulaType>, FormulaTypeValidator>();
            services.AddScoped<IValidator<Formula>, FormulaValidator>();
            services.AddScoped<IValidator<WorkOrder>, WorkOrderValidator>();

            return services;
        }
    }
}
