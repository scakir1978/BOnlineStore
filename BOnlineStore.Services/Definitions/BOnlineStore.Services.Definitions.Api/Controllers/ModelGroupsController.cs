using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelGroupsController : ControllerShared
    {
        private protected IModelGroupService _modelGroupService;                

        public ModelGroupsController(IModelGroupService modelGroupService)
        {
            _modelGroupService = modelGroupService;            
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
