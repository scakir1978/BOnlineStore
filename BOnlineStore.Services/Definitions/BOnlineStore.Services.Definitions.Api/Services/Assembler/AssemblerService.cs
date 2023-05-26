using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class AssemblerService : Service<Assembler, AssemblerDto, AssemblerCreateDto, AssemblerUpdateDto>, IAssemblerService
    {
        public AssemblerService(
            IAssemblerRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Assembler> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
