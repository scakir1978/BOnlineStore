using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;

namespace BOnlineStore.Services.Production.Api.Mappings
{
    public class WorkOrderMappings : Profile
    {
        public WorkOrderMappings()
        {
            CreateMap<WorkOrder, WorkOrderDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<WorkOrder, WorkOrderCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<WorkOrder, WorkOrderUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

