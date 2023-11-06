using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using BOnlineStore.Shared.Constansts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Validations
{
    public class WorkOrderValidator : AbstractValidator<WorkOrder>
    {
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IWorkOrderRepository _repository;

        public WorkOrderValidator(IStringLocalizer<Language> stringLocalizer, IWorkOrderRepository repository)
        {
            _stringLocalizer = stringLocalizer;
            _repository = repository;

            RuleFor(x => x.Description).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.WorkOrderDescriptionNotEmpty]);
            RuleFor(x => x.Description).MaximumLength(500).WithMessage(_stringLocalizer[ProductionApiKeys.WorkOrderDescriptionMaxLength]);

            RuleFor(x => x.WorkOrderNo).NotEmpty().WithMessage(_stringLocalizer[ProductionApiKeys.WorkOrderNoNotEmpty]);
            RuleFor(x => x.WorkOrderNo).MaximumLength(50).WithMessage(_stringLocalizer[ProductionApiKeys.WorkOrderNoMaxLength]);

            //Bu rule sadece kayıt ekleme işlemi sırasında devreye giriyor.
            //Oda service üzerinden kayıt ekleme işlemi olduğunda aşağıdaki gibi bir kod çalıştırılacak sağlanıyor.
            //var validationResult = await _validator.ValidateAsync(entity, options => { options.IncludeRuleSets(GlobalConstants.CodeUniqueControlRule); });
            RuleSet(GlobalConstants.CodeUniqueControlRule, () =>
            {
                RuleFor(x => x).Must(CodeUniqueControl).WithMessage(_stringLocalizer[ProductionApiKeys.WorkOrderNoMustBeUnique]);
            });
        }

        private bool CodeUniqueControl(WorkOrder entity)
        {
            return !_repository.Load().Where(x => x.WorkOrderNo == entity.WorkOrderNo).Any();
        }

    }
}
