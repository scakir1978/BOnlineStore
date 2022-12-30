using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;

namespace BOnlineStore.Services.Production.Api
{
    public class MappingConfigrations
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<FormulaType, FormulaTypeDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<FormulaType, FormulaTypeCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<FormulaType, FormulaTypeUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
