using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class TemplateMappings : Profile
    {
        public TemplateMappings()
        {
            CreateMap<Template, TemplateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Template, TemplateCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Template, TemplateUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
