using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Definitions.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerShared
    {
        private protected IExchangeRateService _ExchangeRateService;
        private protected IMapper _mapper;

        public ExchangeRateController(IExchangeRateService ExchangeRateService, IMapper mapper)
        {
            _ExchangeRateService = ExchangeRateService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<ExchangeRateDto>(_ExchangeRateService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _ExchangeRateService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _ExchangeRateService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ExchangeRateCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _ExchangeRateService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, ExchangeRateUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _ExchangeRateService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _ExchangeRateService.DeleteAsync(id));
        }

    }
}
