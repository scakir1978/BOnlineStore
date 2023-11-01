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

            //Bu rule sadece kayıt ekleme işlemi sırasında devreye giriyor.
            //Oda service üzerinden kayıt ekleme işlemi olduğunda aşağıdaki gibi bir kod çalıştırılacak sağlanıyor.
            //var validationResult = await _validator.ValidateAsync(entity, options => { options.IncludeRuleSets(GlobalConstants.CodeUniqueControlRule); });
            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[ProductionApiKeys.FormulaCodeMustBeUnique]);
            });
        }

        private bool CodeUniqueControl(Formula entity)
        {
            return !_repository.Load().Where(x => x.Code == entity.Code && x.ModelId == entity.ModelId).Any();
        }

    }
}
