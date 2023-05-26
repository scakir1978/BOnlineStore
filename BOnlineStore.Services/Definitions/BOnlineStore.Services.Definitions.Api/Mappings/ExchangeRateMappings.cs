using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ExchangeRateMappings : Profile
    {
        public ExchangeRateMappings()
        {
            CreateMap<ExchangeRate, ExchangeRateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ExchangeRate, ExchangeRateCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ExchangeRate, ExchangeRateUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

