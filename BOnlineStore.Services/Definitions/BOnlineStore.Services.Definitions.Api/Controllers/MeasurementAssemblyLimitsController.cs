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
    public class MeasurementAssemblyLimitsController : ControllerShared
    {
        private protected IMeasurementAssemblyLimitsService _MeasurementAssemblyLimitsService;
        private protected IMapper _mapper;

        public MeasurementAssemblyLimitsController(IMeasurementAssemblyLimitsService MeasurementAssemblyLimitsService, IMapper mapper)
        {
            _MeasurementAssemblyLimitsService = MeasurementAssemblyLimitsService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<MeasurementAssemblyLimitsDto>(_MeasurementAssemblyLimitsService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _MeasurementAssemblyLimitsService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _MeasurementAssemblyLimitsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MeasurementAssemblyLimitsCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _MeasurementAssemblyLimitsService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, MeasurementAssemblyLimitsUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _MeasurementAssemblyLimitsService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _MeasurementAssemblyLimitsService.DeleteAsync(id));
        }

    }
}
