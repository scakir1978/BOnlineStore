using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class PanelMappings : Profile
    {
        public PanelMappings()
        {
            CreateMap<Panel, PanelDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Panel, PanelCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Panel, PanelUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

