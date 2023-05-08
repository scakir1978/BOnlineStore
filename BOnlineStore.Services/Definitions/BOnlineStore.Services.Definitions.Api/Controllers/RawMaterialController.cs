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
    public class RawMaterialController : ControllerShared
    {
        private protected IRawMaterialService _RawMaterialService;
        private protected IMapper _mapper;

        public RawMaterialController(IRawMaterialService RawMaterialService, IMapper mapper)
        {
            _RawMaterialService = RawMaterialService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<RawMaterialDto>(_RawMaterialService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _RawMaterialService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _RawMaterialService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RawMaterialCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _RawMaterialService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, RawMaterialUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _RawMaterialService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _RawMaterialService.DeleteAsync(id));
        }

    }
}
