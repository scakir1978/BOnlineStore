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
            var response = await _userRoleService.GetAllUserRolesAsync();
            if (response.IsSucceed)
            {
                return CreateSuccessActionResultInstance(DataSourceLoader.Load(response.Result, loadOptions));
            }
            return CreateErrorActionResultInstance(response.Result, response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _userRoleService.GetAllUserRolesAsync();
            if (response.IsSucceed)
            {
                return CreateSuccessActionResultInstance(response.Result);
            }
            return CreateErrorActionResultInstance(response.Result, response.Errors);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRolesAsync(string userId)
        {
            var response = await _userRoleService.GetUserRolesAsync(userId);
            if (response.IsSucceed)
            {
                return CreateSuccessActionResultInstance(response.Result);
            }
            return CreateErrorActionResultInstance(response.Result, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUserAsync([FromBody] UserRoleAssignDto input)
        {
            var response = await _userRoleService.AssignRoleToUserAsync(input);

            if (response.IsSucceed)
            {
                return CreateSuccessActionResultInstance(response.Result);
            }

            return CreateErrorActionResultInstance(response.Result, response.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRoleFromUserAsync([FromBody] UserRoleAssignDto input)
        {
            var response = await _userRoleService.RemoveRoleFromUserAsync(input.UserId, input.RoleId);

            if (response.IsSucceed)
                return CreateSuccessActionResultInstance(response.Result);

            return CreateErrorActionResultInstance(response.Result, response.Errors);
        }
    }
}
