using AutoMapper;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelGroupsController : ControllerShared
    {
        private protected IModelGroupService _modelGroupService;
        private protected IMapper _mapper;

        public ModelGroupsController(IModelGroupService modelGroupService, IMapper mapper)
        {
            _modelGroupService = modelGroupService;
            _mapper = mapper;
        }

        [HttpPost("Load")]
        public IActionResult Load(DataSourceLoadOptionsBase loadOptions)
        {
            var data = _modelGroupService.Load().ToList();
            var source = DataSourceLoader.Load(data, loadOptions);
            //var source = DataSourceLoader.Load(_modelGroupService.Load().ToList(), loadOptions);
            //return Ok(DataSourceLoader.Load(_mapper.Map<List<ModelGroupDto>>(_modelGroupService.Load()), loadOptions));

            LoadResult loadResult = new LoadResult
            {
                data = loadOptions.Group == null ? _mapper.Map<List<ModelGroupDto>>(source.data) : source.data,
                totalCount = source.totalCount,                
                groupCount = source.groupCount,
                summary = source.summary
            };

            return Ok(loadResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ModelGroupCreateDto input)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.AddAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ModelGroupUpdateDto input)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.UpdateAsync(id, input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return CreateSuccessActionResultInstance(await _modelGroupService.DeleteAsync(id));
        }

    }
}
