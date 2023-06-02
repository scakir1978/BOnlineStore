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
    public class AssemblyPriceController : ControllerShared
    {
        private protected IAssemblyPriceService _assemblyPriceService;
        private protected IMapper _mapper;

        public AssemblyPriceController(IAssemblyPriceService assemblyPriceService, IMapper mapper)
        {
            _assemblyPriceService = assemblyPriceService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<AssemblyPriceDto>(_assemblyPriceService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _assemblyPriceService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _assemblyPriceService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AssemblyPriceCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _assemblyPriceService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, AssemblyPriceUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _assemblyPriceService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _assemblyPriceService.DeleteAsync(id));
        }

    }
}
