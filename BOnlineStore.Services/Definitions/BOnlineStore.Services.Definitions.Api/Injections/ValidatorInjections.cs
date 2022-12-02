using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Services.Definitions.Api.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.Services.Definitions.Api.Injections
{
    public static partial class Injections
    {
        public static IServiceCollection AddValidatorInjections(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ModelGroup>, ModelGroupValidator>();
            services.AddScoped<IValidator<Color>, ColorValidator>();

            return services;
        }
    }
}
