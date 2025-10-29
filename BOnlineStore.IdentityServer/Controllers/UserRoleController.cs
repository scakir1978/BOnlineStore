using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserRoleService;
using BOnlineStore.Shared.Controllers;
using BOnlineStore.IdentityServer.Dtos.UserRole;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserRoleController : ControllerShared
    {
        private readonly IUserRoleService _userRoleService;        

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;            
        }

        [HttpPost("Load")]
        public async Task<IActionResult> Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            var userRoles = await _userRoleService.GetAllUserRolesAsync();
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(userRoles, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _userRoleService.GetAllUserRolesAsync());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRolesAsync(string userId)
        {
            var userRoles = await _userRoleService.GetUserRolesAsync(userId);
            return CreateSuccessActionResultInstance(userRoles);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUserAsync([FromBody] UserRoleAssignDto input)
        {
            var (userRole, result) = await _userRoleService.AssignRoleToUserAsync(input);

            if (result.Succeeded)
            {
                return CreateSuccessActionResultInstance(userRole);
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("user/{userId}/role/{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            var (userRole, result) = await _userRoleService.RemoveRoleFromUserAsync(userId, roleId);

            if (result.Succeeded)
                return CreateSuccessActionResultInstance(userRole);

            return BadRequest(result.Errors);
        }
    }
}
