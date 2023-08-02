using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class PriceListMasterValidator : AbstractValidator<PriceListMaster>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IPriceListRepository _repository;

        public PriceListMasterValidator(IStringLocalizer<Language> stringLocalizer, IPriceListRepository repository)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.PriceListMasterNameNotEmpty]);
            RuleFor(x => x.Name).MaximumLength(250).WithMessage(_stringLocalizer[DefinitionApiKeys.PriceListMasterNameMaxLength]);

            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.PriceListMasterCodeNotEmpty]);
            RuleFor(x => x.Code).MaximumLength(50).WithMessage(_stringLocalizer[DefinitionApiKeys.PriceListMasterCodeMaxLength]);

            //Bu rule sadece kayıt ekleme işlemi sırasında devreye giriyor.
            //Oda service üzerinden kayıt ekleme işlemi olduğunda aşağıdaki gibi bir kod çalıştırılacak sağlanıyor.
            //var validationResult = await _validator.ValidateAsync(entity, options => { options.IncludeRuleSets(GlobalConstants.CodeUniqueControlRule); });
            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[DefinitionApiKeys.PriceListMasterCodeMustBeUnique]);
            });
        }

        private bool CodeUniqueControl(PriceListMaster entity)
        {
            return !_repository.Load().Where(x => x.Code == entity.Code).Any();
        }

    }
}
