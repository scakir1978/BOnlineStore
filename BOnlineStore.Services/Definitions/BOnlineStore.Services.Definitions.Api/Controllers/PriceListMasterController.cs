using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListMasterController : ControllerShared
    {
        private protected IPriceListMasterService _PriceListMasterService;
        private protected IMapper _mapper;

        public PriceListMasterController(IPriceListMasterService PriceListMasterService, IMapper mapper)
        {
            _PriceListMasterService = PriceListMasterService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<PriceListMasterDto>(_PriceListMasterService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _PriceListMasterService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _PriceListMasterService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PriceListMasterCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _PriceListMasterService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, PriceListMasterUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _PriceListMasterService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _PriceListMasterService.DeleteAsync(id));
        }

    }
}
