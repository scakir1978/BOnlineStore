using BOnlineStore.IdentityServer.Business.RoleService;
using BOnlineStore.IdentityServer.Dtos.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _roleService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var role = await _roleService.GetByNameAsync(name);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleCreateDto input)
        {
            var (role, result) = await _roleService.CreateAsync(input);
            if (result.Succeeded)
            {
                return CreatedAtRoute("GetRoleById", new { id = role.Id }, role);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] RoleUpdateDto input)
        {
            if (id != input.Id) return BadRequest();
            var (role, result) = await _roleService.UpdateAsync(input);
            if (result.Succeeded)
                return Ok(role);
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _roleService.DeleteAsync(id);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result.Errors);
        }
    }
}
