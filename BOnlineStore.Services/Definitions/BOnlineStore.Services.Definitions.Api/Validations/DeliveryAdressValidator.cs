using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class DeliveryAdressValidator : AbstractValidator<DeliveryAdress>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IDeliveryAdressRepository _repository;

        public DeliveryAdressValidator(IStringLocalizer<Language> stringLocalizer, IDeliveryAdressRepository repository)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.DeliveryAdressNameNotEmpty]);
            RuleFor(x => x.Name).MaximumLength(250).WithMessage(_stringLocalizer[DefinitionApiKeys.DeliveryAdressNameMaxLength]);

            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.DeliveryAdressCodeNotEmpty]);
            RuleFor(x => x.Code).MaximumLength(50).WithMessage(_stringLocalizer[DefinitionApiKeys.DeliveryAdressCodeMaxLength]);

            //Bu rule sadece kayıt ekleme işlemi sırasında devreye giriyor.
            //Oda service üzerinden kayıt ekleme işlemi olduğunda aşağıdaki gibi bir kod çalıştırılacak sağlanıyor.
            //var validationResult = await _validator.ValidateAsync(entity, options => { options.IncludeRuleSets(GlobalConstants.CodeUniqueControlRule); });
            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[DefinitionApiKeys.DeliveryAdressCodeMustBeUnique]);
            });
        }

        private bool CodeUniqueControl(DeliveryAdress entity)
        {
            return !_repository.Load().Where(x => x.Code == entity.Code).Any();
        }

    }
}
