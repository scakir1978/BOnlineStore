using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api
{
    public class BankMappings : Profile
    {
        public BankMappings()
        {
            CreateMap<Bank, BankDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Bank, BankCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Bank, BankUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
