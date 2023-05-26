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
    public class AssemblyPriceService : Service<AssemblyPrice, AssemblyPriceDto, AssemblyPriceCreateDto, AssemblyPriceUpdateDto>, IAssemblyPriceService
    {
        public AssemblyPriceService(IAssemblyPriceRepository repository, IMapper mapper,
                                    IStringLocalizer<Language> stringLocalizer, IValidator<AssemblyPrice> validator) :
            base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
