using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace BOnlineStore.Services.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaController : ControllerShared
    {
        private protected IFormulaService _FormulaService;
        private protected IMapper _mapper;

        public FormulaController(IFormulaService FormulaService, IMapper mapper)
        {
            _FormulaService = FormulaService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<FormulaDto>(_FormulaService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _FormulaService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _FormulaService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FormulaCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _FormulaService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, FormulaUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _FormulaService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _FormulaService.DeleteAsync(id));
        }

    }
}
