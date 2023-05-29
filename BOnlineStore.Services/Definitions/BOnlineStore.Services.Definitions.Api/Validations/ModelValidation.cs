using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ModelValidator : AbstractValidator<Model>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IModelRepository _repository;

        public ModelValidator(IStringLocalizer<Language> stringLocalizer, IModelRepository repository)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelNameNotEmpty]);
            RuleFor(x => x.Code).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelCodeNotEmpty]);
            RuleFor(x => x.ModelGroupId).NotEmpty().WithMessage(_stringLocalizer[DefinitionApiKeys.ModelGroupNotEmpty]);

            //Bu rule sadece kayıt ekleme işlemi sırasında devreye giriyor.
            //Oda service üzerinden kayıt ekleme işlemi olduğunda aşağıdaki gibi bir kod çalıştırılacak sağlanıyor.
            //var validationResult = await _validator.ValidateAsync(entity, options => { options.IncludeRuleSets(GlobalConstants.CodeUniqueControlRule); });
            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[DefinitionApiKeys.ModelCodeMustBeUnique]);
            });

        }

        private bool CodeUniqueControl(Model entity)
        {
            return !_repository.Load().Where(x => x.Code == entity.Code).Any();
        }

    }
}
