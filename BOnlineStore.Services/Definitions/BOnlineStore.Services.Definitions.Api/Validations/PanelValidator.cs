using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api
{
    public class PanelValidator : AbstractValidator<Panel>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public PanelValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.PanelNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.PanelCodeNotEmpty]);
        }

    }
}
