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
    public class BankService : Service<Bank, BankDto, BankCreateDto, BankUpdateDto>, IBankService
    {
        public BankService(
            IBankRepository repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer,
            IValidator<Bank> validator)
            : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
