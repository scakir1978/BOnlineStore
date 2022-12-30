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

            return services;
        }
    }
}
