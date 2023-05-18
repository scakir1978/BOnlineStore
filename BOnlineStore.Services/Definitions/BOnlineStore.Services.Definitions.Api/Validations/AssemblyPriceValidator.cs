using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api
{
    public class AssemblyPriceValidator : AbstractValidator<AssemblyPrice>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public AssemblyPriceValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.RegionId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.RegionCodeNotEmpty]);
            RuleFor(x => x.GlassId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.GlassCodeNotEmpty]);
        }

    }
}
