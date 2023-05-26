using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class MeasurementAssemblyLimitsValidator : AbstractValidator<MeasurementAssemblyLimits>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public MeasurementAssemblyLimitsValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Day).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.MeasurementAssemblyLimitsDayNotEmpty]);
            RuleFor(x => x.AssemblerId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.MeasurementAssemblyLimitsAssemblerNotEmpty]);
            RuleFor(x => x.AssemblyQuantity).NotEmpty()
                .When(x => x.MeasurementQuantity == null && x.MeasurementQuantity == 0)
                .WithMessage(_stringLocalizer[DefinitionApiKeys.MeasurementAssemblyLimitsQuantitiesNotEmpty]);
            RuleFor(x => x.MeasurementQuantity).NotEmpty()
                .When(x => x.AssemblyQuantity == null || x.AssemblyQuantity == 0)
                .WithMessage(_stringLocalizer[DefinitionApiKeys.MeasurementAssemblyLimitsQuantitiesNotEmpty]);
            RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Region01Id) ||
                                      !string.IsNullOrWhiteSpace(x.Region02Id) ||
                                      !string.IsNullOrWhiteSpace(x.Region03Id) ||
                                      !string.IsNullOrWhiteSpace(x.Region04Id) ||
                                      !string.IsNullOrWhiteSpace(x.Region05Id))
                .WithMessage(_stringLocalizer[DefinitionApiKeys.MeasurementAssemblyLimitsRegionsNotEmpty]); ;


        }

    }
}
