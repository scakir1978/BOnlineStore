using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class AssemblerMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Assembler, AssemblerDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Assembler, AssemblerCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Assembler, AssemblerUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
