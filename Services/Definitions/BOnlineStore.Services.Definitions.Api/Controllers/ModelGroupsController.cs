using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelGroupsController : ControllerShared
    {
        private protected IModelGroupService _modelGroupService;
        private protected ILogger<ModelGroupsController> _logger;

        public ModelGroupsController(IModelGroupService modelGroupService, ILogger<ModelGroupsController> logger)
        {
            _modelGroupService = modelGroupService;
            _logger = logger;
        }

        [HttpGet("Load")]
        public IActionResult Load()
        {
            return CreateSuccessActionResultInstance(_modelGroupService.Load());            
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.GetAsync());            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.GetByIdAsync(id));            
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ModelGroupCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.AddAsync(input));            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ModelGroupUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.UpdateAsync(id, input));            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.DeleteAsync(id));            
        }

    }
}
