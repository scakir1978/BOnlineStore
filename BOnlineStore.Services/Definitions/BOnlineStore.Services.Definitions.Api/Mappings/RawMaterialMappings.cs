using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api
{
    public class RawMaterialMappings : Profile
    {
        public RawMaterialMappings()
        {
            CreateMap<RawMaterial, RawMaterialDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RawMaterial, RawMaterialCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RawMaterial, RawMaterialUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

