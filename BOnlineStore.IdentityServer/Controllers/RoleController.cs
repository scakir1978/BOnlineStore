using AutoMapper;
using BOnlineStore.IdentityServer.Business.RoleService;
using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class RoleController : ControllerShared
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public async Task<IActionResult> Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            var roles = await _roleService.GetAllAsync();
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(roles, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _roleService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await _roleService.GetByIdAsync(id);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<RoleDto>(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var response = await _roleService.GetByNameAsync(name);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<RoleDto>(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleCreateDto input)
        {
            var response = await _roleService.CreateAsync(input);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<RoleDto>(null, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, RoleUpdateDto input)
        {
            var response = await _roleService.UpdateAsync(id, input);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<RoleDto>(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _roleService.DeleteAsync(id);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<RoleDto>(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }
    }
}
