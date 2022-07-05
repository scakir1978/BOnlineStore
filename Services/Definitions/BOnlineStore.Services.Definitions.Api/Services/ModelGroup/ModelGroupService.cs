using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class ModelGroupService : Service<ModelGroup,ModelGroupDto,ModelGroupCreateDto,ModelGroupUpdateDto> , IModelGroupService
    {
        private readonly IRepository<ModelGroup> _repository;
        private readonly IMapper _mapper;

        public ModelGroupService(IRepository<ModelGroup> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
    }
}
