using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class FirmMappings : Profile
    {
        public FirmMappings()
        {
            CreateMap<Firm, FirmDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Firm, FirmForComboDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Firm, FirmCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Firm, FirmUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

