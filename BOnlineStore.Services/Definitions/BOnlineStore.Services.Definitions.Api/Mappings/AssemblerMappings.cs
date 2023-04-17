using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class AssemblerMappings : Profile
    {
        public AssemblerMappings()
        {
            CreateMap<Assembler, AssemblerDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Assembler, AssemblerCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Assembler, AssemblerUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
