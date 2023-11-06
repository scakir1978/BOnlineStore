using AutoMapper;
using $rootnamespace$.Dtos;
using $rootnamespace$.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace $rootnamespace$.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class $className$Controller : ControllerShared
    {
		
        private protected I$className$Service _$className$Service;
        private protected IMapper _mapper;

		public $safeitemname$(I$className$Service $className$Service, IMapper mapper)
        {
            _$className$Service = $className$Service;
            _mapper = mapper;
        }

		[HttpPost("Load")]
		public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
		{
			loadOptions.StringToLower = true;
			return CreateSuccessActionResultInstance(DataSourceLoader.Load(_mapper.ProjectTo <$className$Dto > (_$className$Service.Load()), loadOptions));
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			return CreateSuccessActionResultInstance(await _$className$Service.GetAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(string id)
		{
			return CreateSuccessActionResultInstance(await _$className$Service.GetByIdAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync($className$CreateDto input)
		{
			return CreateSuccessActionResultInstance(await _$className$Service.AddAsync(input));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(string id, $className$UpdateDto input)
		{
			return CreateSuccessActionResultInstance(await _$className$Service.UpdateAsync(id, input));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(string id)
		{
			return CreateSuccessActionResultInstance(await _$className$Service.DeleteAsync(id));
		}
		
    }
}
