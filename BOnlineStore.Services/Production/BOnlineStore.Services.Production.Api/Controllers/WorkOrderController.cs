using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerShared
    {
        private protected IWorkOrderService _workOrderService;
        private protected IMapper _mapper;

        public WorkOrderController(IWorkOrderService workOrderService, IMapper mapper)
        {
            _workOrderService = workOrderService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.Map<List<WorkOrderDto>>(_workOrderService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _workOrderService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(WorkOrderCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, WorkOrderUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.DeleteAsync(id));
        }

        [HttpGet("CalculateProductionList")]
        public async Task<IActionResult> CalculateProductionList(string workOrderId)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.CalculateProductionList(workOrderId));
        }

    }
}
