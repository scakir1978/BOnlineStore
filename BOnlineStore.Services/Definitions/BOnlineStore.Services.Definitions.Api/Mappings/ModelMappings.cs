using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ModelMappings : Profile
    {
        public ModelMappings()
        {
            CreateMap<Model, ModelDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Model, ModelForComboDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Model, ModelCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Model, ModelUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
