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
    public class RecipeTypeController : ControllerShared
    {
        private protected IRecipeTypeService _RecipeTypeService;
        private protected IMapper _mapper;

        public RecipeTypeController(IRecipeTypeService RecipeTypeService, IMapper mapper)
        {
            _RecipeTypeService = RecipeTypeService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.Map<List<RecipeTypeDto>>(_RecipeTypeService.Load().ToList()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _RecipeTypeService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _RecipeTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecipeTypeCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _RecipeTypeService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, RecipeTypeUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _RecipeTypeService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _RecipeTypeService.DeleteAsync(id));
        }

    }
}
