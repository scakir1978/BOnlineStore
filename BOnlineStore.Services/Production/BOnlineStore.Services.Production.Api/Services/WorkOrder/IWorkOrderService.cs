using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using System.Linq.Expressions;

namespace BOnlineStore.Services.Production.Api.Services
{
    public interface IWorkOrderService : IService<WorkOrder, WorkOrderDto, WorkOrderCreateDto, WorkOrderUpdateDto>
    {
    }
}
