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
    public class MeasurementAssemblyLimitsController : ControllerShared
    {
        private protected IMeasurementAssemblyLimitsService _measurementAssemblyLimitsService;
        private protected IMapper _mapper;

        public MeasurementAssemblyLimitsController(IMeasurementAssemblyLimitsService measurementAssemblyLimitsService, IMapper mapper)
        {
            _measurementAssemblyLimitsService = measurementAssemblyLimitsService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<MeasurementAssemblyLimitsDto>(_measurementAssemblyLimitsService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _measurementAssemblyLimitsService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _measurementAssemblyLimitsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MeasurementAssemblyLimitsCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _measurementAssemblyLimitsService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, MeasurementAssemblyLimitsUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _measurementAssemblyLimitsService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _measurementAssemblyLimitsService.DeleteAsync(id));
        }

    }
}
