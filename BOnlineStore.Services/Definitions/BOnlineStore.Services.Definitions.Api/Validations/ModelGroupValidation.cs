using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Dtos;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ModelGroupCreateValidation : AbstractValidator<ModelGroupCreateDto>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public ModelGroupCreateValidation(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupCodeNotEmpty]);
        }

    }
    public class ModelGroupUpdateValidation : AbstractValidator<ModelGroupUpdateDto>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public ModelGroupUpdateValidation(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupCodeNotEmpty]);

        }
    }
}
