using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Dtos.Formula;
using BOnlineStore.Services.Production.Api.Entities;
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
        private protected IFormulaService _formulaService;
        private protected IMapper _mapper;

        public FormulaController(IFormulaService FormulaService, IMapper mapper)
        {
            _formulaService = FormulaService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.Map<List<FormulaLoadDto>>(_formulaService.Load()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _formulaService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _formulaService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FormulaCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _formulaService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, FormulaUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _formulaService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _formulaService.DeleteAsync(id));
        }

        [HttpPost("ExecuteFormulaTest")]
        public async Task<IActionResult> ExecuteFormulaTest(List<FormulaDetailDto> formulaDetailsDto)
        {
            var isExecuted = await _formulaService.ExecuteFormulaTest(_mapper.Map<List<FormulaDetail>>(formulaDetailsDto));
            return CreateSuccessActionResultInstance(isExecuted);
        }

        [HttpPost("CopyFormula")]
        public async Task<IActionResult> CopyFormula(FormulaCopyDto? input)
        {
            var result = await _formulaService.CopyFormula(input?.FormulaId ?? "", input?.FormulaCode ?? "", input?.ModelId ?? "");
            return CreateSuccessActionResultInstance(result);
        }

    }
}
