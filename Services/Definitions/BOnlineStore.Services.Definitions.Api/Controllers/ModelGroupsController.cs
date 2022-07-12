using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelGroupsController : ControllerBase
    {
        private protected IModelGroupService _modelGroupService;

        public ModelGroupsController(IModelGroupService modelGroupService)
        {
            _modelGroupService = modelGroupService;
        }

        [HttpGet("Load")]
        public IActionResult Load()
        {
            return Ok(_modelGroupService.Load());
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _modelGroupService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _modelGroupService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ModelGroupCreateDto input)
        {
            return Ok(await _modelGroupService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ModelGroupUpdateDto input)
        {
            return Ok(await _modelGroupService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _modelGroupService.DeleteAsync(id));
        }

    }
}
