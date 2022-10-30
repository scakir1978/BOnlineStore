using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ModelGroupValidator : AbstractValidator<ModelGroup>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public ModelGroupValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupCodeNotEmpty]);
        }

    }
}
