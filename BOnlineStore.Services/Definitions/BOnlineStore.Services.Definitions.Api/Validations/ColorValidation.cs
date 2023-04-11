using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ColorValidator : AbstractValidator<Color>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public ColorValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ColorNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ColorCodeNotEmpty]);
            RuleFor(x => x.ColorGroupId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ColorGroupNotEmpty]);
        }

    }
}
