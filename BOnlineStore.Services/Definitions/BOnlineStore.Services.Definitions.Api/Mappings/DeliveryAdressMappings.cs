using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class DeliveryAdressMappings : Profile
    {
        public DeliveryAdressMappings()
        {
            CreateMap<DeliveryAdress, DeliveryAdressDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<DeliveryAdress, DeliveryAdressCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<DeliveryAdress, DeliveryAdressUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

