using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class CountryMappings : Profile
    {
        public CountryMappings()
        {
            CreateMap<Country, CountryDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Country, CountryCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Country, CountryUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

