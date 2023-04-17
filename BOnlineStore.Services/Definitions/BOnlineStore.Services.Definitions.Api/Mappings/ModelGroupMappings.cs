using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ModelGroupMappings : Profile
    {
        public ModelGroupMappings()
        {
            CreateMap<ModelGroup, ModelGroupDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ModelGroup, ModelGroupCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<ModelGroup, ModelGroupUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
