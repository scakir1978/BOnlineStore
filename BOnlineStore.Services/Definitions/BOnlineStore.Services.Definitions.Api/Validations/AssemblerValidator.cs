using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class AssemblerValidator : AbstractValidator<Assembler>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public AssemblerValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.AssemblerNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.AssemblerCodeNotEmpty]);
        }

    }
}
