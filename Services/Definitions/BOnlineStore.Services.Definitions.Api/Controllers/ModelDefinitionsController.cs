using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ModelDefinitionsController : ControllerBase
    {
        private protected IModelGroupService _modelGroupService;

        public ModelDefinitionsController(IModelGroupService modelGroupService)
        {
            _modelGroupService = modelGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok(User.Claims.FirstOrDefault(x => x.Type == "tenantId")?.Value ?? "Tenant id bulunamadı.");
            return Ok(_modelGroupService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelGroupCreateDto input)
        {
            return Ok(await _modelGroupService.AddAsync(input));
        }

    }
}
