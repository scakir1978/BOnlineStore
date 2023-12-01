using BOnlineStore.BFF.Api.Services.Production;
using BOnlineStore.Shared.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.BFF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreController : ControllerShared
    {
        private protected IWorkOrderService _workOrderService;

        public OnlineStoreController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpGet("CalculateProductionList")]
        public async Task<IActionResult> CalculateProductionList(string workOrderId)
        {
            return CreateSuccessActionResultInstance(await _workOrderService.CalculateProductionList(workOrderId));
        }
    }
}
