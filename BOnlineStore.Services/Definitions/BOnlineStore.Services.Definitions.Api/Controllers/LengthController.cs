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
    public class LengthController : ControllerShared
    {
        private protected ILengthService _lengthService;
        private protected IMapper _mapper;

        public LengthController(ILengthService lengthService, IMapper mapper)
        {
            _lengthService = lengthService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<LengthDto>(_lengthService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _lengthService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _lengthService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LengthCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _lengthService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, LengthUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _lengthService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _lengthService.DeleteAsync(id));
        }

    }
}
