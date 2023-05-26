using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class AssemblyPriceMappings : Profile
    {
        public AssemblyPriceMappings()
        {
            CreateMap<AssemblyPrice, AssemblyPriceDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<AssemblyPrice, AssemblyPriceCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<AssemblyPrice, AssemblyPriceUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

