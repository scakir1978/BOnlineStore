using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class CurrencyMappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Currency, CurrencyDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Currency, CurrencyCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
                config.CreateMap<Currency, CurrencyUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            });

            return mappingConfig;
        }
    }
}
