using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class CountyMappings : Profile
    {
        public CountyMappings()
        {
            CreateMap<County, CountyDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<County, CountyCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<County, CountyUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

