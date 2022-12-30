using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Validations
{
    public class FormulaTypeValidator : AbstractValidator<FormulaType>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        public FormulaTypeValidator(IStringLocalizer<Language> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.FormulaTypeNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.FormulaTypeCodeNotEmpty]);
        }

    }
}
