using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ModelDefinitionsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(User.Claims.FirstOrDefault(x => x.Type == "tenantId")?.Value ?? "Tenant id bulunamadı.");
        }

    }
}
