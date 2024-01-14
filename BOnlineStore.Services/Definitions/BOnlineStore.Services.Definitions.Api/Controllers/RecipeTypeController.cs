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
    public class RecipeTypeController : ControllerShared
    {
        private protected IRecipeTypeService _recipeTypeService;
        private protected IMapper _mapper;

        public RecipeTypeController(IRecipeTypeService recipeTypeService, IMapper mapper)
        {
            _recipeTypeService = recipeTypeService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.Map<List<RecipeTypeDto>>(_recipeTypeService.Load()), loadOptions));
        }

        [HttpPost("LoadForCombo")]
        public IActionResult LoadForCombo(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.Map<List<RecipeTypeForComboDto>>(_recipeTypeService.Load().ToList()), loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _recipeTypeService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _recipeTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecipeTypeCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _recipeTypeService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, RecipeTypeUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _recipeTypeService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateSuccessActionResultInstance(await _recipeTypeService.DeleteAsync(id));
        }

    }
}
