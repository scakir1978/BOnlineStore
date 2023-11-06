using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class WorkOrderService : Service<WorkOrder, WorkOrderDto, WorkOrderCreateDto, WorkOrderUpdateDto>, IWorkOrderService
    {
        public WorkOrderService(
            IWorkOrderRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<WorkOrder> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
