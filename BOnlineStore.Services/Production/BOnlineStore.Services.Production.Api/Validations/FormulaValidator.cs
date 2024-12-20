using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Validations
{
    public class FormulaValidator : AbstractValidator<Formula>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IFormulaRepository _repository;

        public FormulaValidator(IStringLocalizer<Language> stringLocalizer, IFormulaRepository repository)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.FormulaNameNotEmpty]);
            RuleFor(x => x.Name).MaximumLength(250).WithMessage(_stringLocalizer[ProductionApiKeys.FormulaNameMaxLength]);

            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.FormulaCodeNotEmpty]);
            RuleFor(x => x.Code).MaximumLength(50).WithMessage(_stringLocalizer[ProductionApiKeys.FormulaCodeMaxLength]);

            //RuleFor(x => x.ModelId).NotNull().When(x => x.PanelId != null).WithMessage(_stringLocalizer[ProductionApiKeys.ModelIdAndPanelIdCannotBeBothSet]);

            RuleFor(x => x).Must(x => !(x.ModelId != null && x.PanelId != null))
                .WithMessage(_stringLocalizer[ProductionApiKeys.ModelIdAndPanelIdCannotBeBothSet]);

            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[ProductionApiKeys.FormulaCodeMustBeUnique]);

            });
        }

        private bool CodeUniqueControl(Formula entity)
        {
            if (entity.PanelId !=null)
                return !_repository.Load().Where(x => x.Code == entity.Code && x.PanelId == entity.PanelId).Any();

            return !_repository.Load().Where(x => x.Code == entity.Code && x.ModelId == entity.ModelId).Any();

        }
    }
}
