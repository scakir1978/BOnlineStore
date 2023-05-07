using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api
{
    public class FirmTypeMappings : Profile
    {
        public FirmTypeMappings()
        {
            CreateMap<FirmType, FirmTypeDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<FirmType, FirmTypeCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<FirmType, FirmTypeUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

