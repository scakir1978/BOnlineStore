using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace $rootnamespace$.Mappings
{
    public class $className$Mappings : Profile
    {
        public $className$Mappings()
        {
            CreateMap<$className$, $className$Dto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<$className$, $className$CreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<$className$, $className$UpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

