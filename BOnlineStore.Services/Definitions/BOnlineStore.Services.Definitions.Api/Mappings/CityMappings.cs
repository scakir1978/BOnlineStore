using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class CityMappings : Profile
    {
        public CityMappings()
        {
            CreateMap<City, CityDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<City, CityCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<City, CityUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

