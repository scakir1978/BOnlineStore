using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ExchangeRateValidator : AbstractValidator<ExchangeRate>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public ExchangeRateValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.ExcangeRateDate).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ExchangeRateDateNotEmpty]);
            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.CurrencyIdNotEmpty]);
        }

    }
}
