using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class AssemblerService : Service<Assembler, AssemblerDto, AssemblerCreateDto, AssemblerUpdateDto>, IAssemblerService
    {
        private readonly IAssemblerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public AssemblerService(IAssemblerRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer) : base(repository, mapper, stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

    }
}
