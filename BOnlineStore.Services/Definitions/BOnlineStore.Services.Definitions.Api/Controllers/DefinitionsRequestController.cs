using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Services.Definitions.Api.Services.Shared;
using BOnlineStore.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefinitionsRequestController : ControllerShared
    {
        private protected IDefinitionsRequestService _definitionsRequestService;

        public DefinitionsRequestController(IDefinitionsRequestService definitionsRequestService)
        {
            _definitionsRequestService = definitionsRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> GetByIdAsync(List<DefinitionsRequestDto> input)
        {
            return CreateSuccessActionResultInstance(await _definitionsRequestService.GetByIdAsync(input));
        }
    }
}
