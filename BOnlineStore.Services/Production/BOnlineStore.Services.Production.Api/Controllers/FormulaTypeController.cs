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
    public class FormulaTypeController : ControllerShared
    {
        private protected IFormulaTypeService _formulaTypeService;
        private protected IMapper _mapper;

        public FormulaTypeController(IFormulaTypeService formulaTypeService, IMapper mapper)
        {
            _formulaTypeService = formulaTypeService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo<FormulaTypeDto>(_formulaTypeService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _formulaTypeService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _formulaTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FormulaTypeCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _formulaTypeService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, FormulaTypeUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _formulaTypeService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _formulaTypeService.DeleteAsync(id));
        }

    }
}
