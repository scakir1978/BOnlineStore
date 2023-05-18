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
    public class AssemblyPriceController : ControllerShared
    {
        private protected IAssemblyPriceService _AssemblyPriceService;
        private protected IMapper _mapper;

        public AssemblyPriceController(IAssemblyPriceService AssemblyPriceService, IMapper mapper)
        {
            _AssemblyPriceService = AssemblyPriceService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<AssemblyPriceDto>(_AssemblyPriceService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _AssemblyPriceService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _AssemblyPriceService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AssemblyPriceCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _AssemblyPriceService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, AssemblyPriceUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _AssemblyPriceService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _AssemblyPriceService.DeleteAsync(id));
        }

    }
}
