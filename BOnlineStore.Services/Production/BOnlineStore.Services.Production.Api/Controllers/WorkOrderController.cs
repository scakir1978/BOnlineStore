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
        private protected IWorkOrderService _WorkOrderService;
        private protected IMapper _mapper;

        public WorkOrderController(IWorkOrderService WorkOrderService, IMapper mapper)
        {
            _WorkOrderService = WorkOrderService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<WorkOrderDto>(_WorkOrderService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _WorkOrderService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _WorkOrderService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(WorkOrderCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _WorkOrderService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, WorkOrderUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _WorkOrderService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _WorkOrderService.DeleteAsync(id));
        }

    }
}
