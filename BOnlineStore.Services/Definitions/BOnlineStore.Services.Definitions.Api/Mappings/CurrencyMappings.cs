using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class CurrencyMappings : Profile
    {
        public CurrencyMappings()
        {
            CreateMap<Currency, CurrencyDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Currency, CurrencyCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Currency, CurrencyUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
