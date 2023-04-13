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
    public class RawMaterialGroupController : ControllerShared
    {
        private protected IRawMaterialGroupService _rawMaterialGroupService;
        private protected IMapper _mapper;

        public RawMaterialGroupController(IRawMaterialGroupService rawMaterialGroupService, IMapper mapper)
        {
            _rawMaterialGroupService = rawMaterialGroupService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<RawMaterialGroupDto>(_rawMaterialGroupService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _rawMaterialGroupService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _rawMaterialGroupService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RawMaterialGroupCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _rawMaterialGroupService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, RawMaterialGroupUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _rawMaterialGroupService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _rawMaterialGroupService.DeleteAsync(id));
        }

    }
}
